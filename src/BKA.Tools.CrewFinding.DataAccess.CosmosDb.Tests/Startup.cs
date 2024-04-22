using System;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.Players;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings.KeyVault;
using BKA.Tools.CrewFinding.Players.Ports;
using Microsoft.Azure.Cosmos;
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

    private static KeySecretsProvider CreateKeySecretsProvider(IConfigurationRoot config)
    {
        var secretClientOptions = new SecretClientOptions();
        secretClientOptions.AddPolicy(new KeyVaultProxyPolicy(), HttpPipelinePosition.PerCall);
        var secretClient = new SecretClient(new Uri(config["KeyVault:endpoint"] ?? string.Empty),
            new DefaultAzureCredential(),
            secretClientOptions);

        return new KeySecretsProvider(secretClient);
    }

    private static DatabaseSettingsProvider CreateDatabaseSettingsProvider(
        IKeySecretsProvider keySecretsProvider,
        IConfigurationRoot config)
    {
        return new DatabaseSettingsProvider(keySecretsProvider, config);
    }

    private static void RegisterDependencies(
        IServiceCollection services,
        IDatabaseSettingsProvider<Container> databaseSettingsProvider)
    {
        services.AddTransient<IDatabaseSettingsProvider<Container>>(_ =>
            databaseSettingsProvider);
        services.AddTransient<ICrewCommandRepository>(_ =>
            new CrewCommandRepository(databaseSettingsProvider.GetCrewContainer()));

        services.AddTransient<IPlayerCommandRepository>(_ =>
            new PlayerCommands(databaseSettingsProvider.GetPlayerContainer()));
    }
}