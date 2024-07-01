using System;
using BKA.Tools.CrewFinding.Azure.DataBase.CrewParties;
using BKA.Tools.CrewFinding.Azure.DataBase.Players;
using BKA.Tools.CrewFinding.Azure.DataBase.VoiceChannels;
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
        var keySecretsProvider = KeyVaultInitializer.CreateKeySecretsProvider(config["keyVault:endpoint"]!);
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
        var disbandedCrewsContainer = databaseSettingsProvider.GetDisbandedCrewsContainer();
        var voiceChannelContainer = databaseSettingsProvider.GetVoiceChannelContainer();

        services.AddTransient<ICrewCommandRepository>(_ =>
            new CrewCommandRepository(crewContainer, disbandedCrewsContainer));

        const int minNameLength = 2;
        const int maxNameLength = 16;
        services.AddTransient<ICrewValidationRepository>(_ =>
            new CrewQueryRepository(crewContainer, minNameLength, maxNameLength));

        services.AddTransient<ICrewQueryRepository>(_ =>
            new CrewQueryRepository(crewContainer, minNameLength, maxNameLength));

        services.AddTransient<IPlayerCommandRepository>(_ =>
            new PlayerCommands(databaseSettingsProvider.GetPlayerContainer()));

        services.AddTransient<IPlayerQueryRepository>(_ =>
            new PlayerQueries(playerContainer, minNameLength, maxNameLength));

        services.AddTransient<ICrewDisbandRepository>(_ =>
            new CrewDisbandRepository(crewContainer, disbandedCrewsContainer));

        services.AddTransient<IVoiceChannelCommandRepository>(_ =>
            new VoiceChannelCommandRepository(voiceChannelContainer));

        services.AddTransient<IVoiceChannelQueryRepository>(_ =>
            new VoiceChannelQueryRepository(voiceChannelContainer));
    }
}