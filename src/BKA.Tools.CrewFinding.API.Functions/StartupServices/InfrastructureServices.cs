using BKA.Tools.CrewFinding.Azure.DataBase;
using BKA.Tools.CrewFinding.Azure.DataBase.CrewParties;
using BKA.Tools.CrewFinding.Azure.DataBase.Players;
using BKA.Tools.CrewFinding.Azure.DataBase.VoiceChannels;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Discord;
using BKA.Tools.CrewFinding.KeyVault;
using BKA.Tools.CrewFinding.Players.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace BKA.Tools.CrewFinding.API.Functions.StartupServices;

public static class InfrastructureServices
{
    public static void AddRepositories(IServiceCollection service, KeySecretProvider keySecretsProvider)
    {
        AddDataBaseServices(service, ContainerBuilderService.CreateContainerBuilder(keySecretsProvider));
        AddDiscordServices(service, keySecretsProvider);
    }

    private static void AddDiscordServices(IServiceCollection service, KeySecretProvider keySecretsProvider)
    {
        var token = keySecretsProvider.GetSecret(Configuration.GetEnvironmentVariable("keyVaultAzureDiscordTokenName"));

        var discordSettings = new DiscordSettings(token,
            long.Parse(Configuration.GetEnvironmentVariable("discordGuildId")),
            long.Parse(Configuration.GetEnvironmentVariable("discordParentId")));

        service.AddSingleton<IVoiceChannelHandler>(_ => new VoiceChannelHandler(discordSettings));
    }

    private static void AddDataBaseServices(IServiceCollection service,
        CosmosDbContainerBuilder cosmosDbContainerBuilder)
    {
        var databaseId = Configuration.GetEnvironmentVariable("cosmosDBDatabase");
        var minCitizenNameLength = int.Parse(Configuration.GetEnvironmentVariable("minCitizenNameLength"));
        var maxCitizenNameLength = int.Parse(Configuration.GetEnvironmentVariable("maxCitizenNameLength"));

        var crewContainer =
            cosmosDbContainerBuilder.Build(databaseId, Configuration.GetEnvironmentVariable("cosmosDBCrewContainer"));

        var disbandedCrewsContainer =
            cosmosDbContainerBuilder.Build(databaseId,
                Configuration.GetEnvironmentVariable("cosmosDBDisbandedCrewsContainer"));

        var playerContainer =
            cosmosDbContainerBuilder.Build(databaseId, Configuration.GetEnvironmentVariable("cosmosDBPlayerContainer"));

        var voiceChannelContainer =
            cosmosDbContainerBuilder.Build(databaseId,
                Configuration.GetEnvironmentVariable("cosmosDBVoiceChannelContainer"));

        service.AddSingleton<ICrewCommandRepository>(_ =>
            new CrewCommandRepository(crewContainer, disbandedCrewsContainer));
        service.AddSingleton<ICrewQueryRepository>(_ =>
            new CrewQueryRepository(crewContainer, minCitizenNameLength, maxCitizenNameLength));
        service.AddSingleton<ICrewValidationRepository>(_ =>
            new CrewQueryRepository(crewContainer, minCitizenNameLength, maxCitizenNameLength));
        service.AddSingleton<IPlayerQueryRepository>(_ =>
            new PlayerQueries(playerContainer, minCitizenNameLength, maxCitizenNameLength));
        service.AddSingleton<IPlayerCommandRepository>(_ => new PlayerCommands(playerContainer));
        service.AddSingleton<ICrewDisbandRepository>(_ =>
            new CrewDisbandRepository(crewContainer, disbandedCrewsContainer));
        service.AddSingleton<IVoiceChannelCommandRepository>(_ =>
            new VoiceChannelCommandRepository(voiceChannelContainer));
        service.AddSingleton<IVoiceChannelQueryRepository>(_ =>
            new VoiceChannelQueryRepository(voiceChannelContainer));
    }
}