using System;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.Players;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;
using BKA.Tools.CrewFinding.KeyVault;
using BKA.Tools.CrewFinding.Players.Ports;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var config = BuildConfiguration();

        var keySecretsProvider = CreateKeySecretsProvider(config);
        var databaseSettingsProvider = CreateDatabaseSettingsProvider(keySecretsProvider, config);

        RegisterDependencies(services, databaseSettingsProvider);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", false, true)
            .Build();
    }

    private static KeySecretProvider CreateKeySecretsProvider(IConfigurationRoot config)
    {
        var secretClientOptions = new SecretClientOptions();
        secretClientOptions.AddPolicy(new KeyVaultProxyPolicy(), HttpPipelinePosition.PerCall);
        var secretClient = new SecretClient(new Uri(config["KeyVault:endpoint"] ?? string.Empty),
            new DefaultAzureCredential(),
            secretClientOptions);

        return new KeySecretProvider(secretClient);
    }

    private static DatabaseSettingsProvider CreateDatabaseSettingsProvider(
        IKeySecretProvider keySecretProvider,
        IConfigurationRoot config)
    {
        return new DatabaseSettingsProvider(keySecretProvider, config);
    }

    private static void RegisterDependencies(
        IServiceCollection services,
        IDatabaseSettingsProvider<Container> databaseSettingsProvider)
    {
        services.AddTransient<IDatabaseSettingsProvider<Container>>(_ =>
            databaseSettingsProvider);

        var crewContainer = databaseSettingsProvider.GetCrewContainer();
        var playerContainer = databaseSettingsProvider.GetPlayerContainer();
        
        services.AddTransient<ICrewCommandRepository>(_ =>
            new CrewCommandRepository(crewContainer));

        services.AddTransient<ICrewValidationRepository>(_ =>
            new CrewValidationRepository(crewContainer));
        
        services.AddTransient<ICrewQueryRepository>(_ =>
            new CrewValidationRepository(crewContainer));
        
        services.AddTransient<IPlayerCommandRepository>(_ =>
            new PlayerCommands(databaseSettingsProvider.GetPlayerContainer()));

        services.AddTransient<IPlayerQueryRepository>(_ =>
            new PlayerQueries(playerContainer));
    }
}