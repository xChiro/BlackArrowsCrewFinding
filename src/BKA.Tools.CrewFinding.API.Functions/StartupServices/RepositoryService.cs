using BKA.Tools.CrewFinding.Azure.DataBase;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.Players;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace BKA.Tools.CrewFinding.API.Functions.StartupServices;

public static class RepositoryService
{
    public static void AddRepositories(IServiceCollection service, ContainerBuilder containerBuilder)
    {
        var databaseId = Configuration.GetEnvironmentVariable("cosmosDBDatabase");
        var minCitizenNameLength = int.Parse(Configuration.GetEnvironmentVariable("minCitizenNameLength"));
        var maxCitizenNameLength = int.Parse(Configuration.GetEnvironmentVariable("maxCitizenNameLength"));

        var crewContainer =
            containerBuilder.Build(databaseId, Configuration.GetEnvironmentVariable("cosmosDBCrewContainer"));

        var disbandedCrewsContainer =
            containerBuilder.Build(databaseId, Configuration.GetEnvironmentVariable("cosmosDBDisbandedCrewsContainer"));

        var playerContainer =
            containerBuilder.Build(databaseId, Configuration.GetEnvironmentVariable("cosmosDBPlayerContainer"));

        service.AddSingleton<ICrewCommandRepository>(_ => new CrewCommandRepository(crewContainer));
        service.AddSingleton<ICrewQueryRepository>(_ => new CrewQueryRepository(crewContainer, minCitizenNameLength, maxCitizenNameLength));
        service.AddSingleton<ICrewValidationRepository>(_ => new CrewQueryRepository(crewContainer, minCitizenNameLength, maxCitizenNameLength));
        service.AddSingleton<IPlayerQueryRepository>(_ =>
            new PlayerQueries(playerContainer, minCitizenNameLength, maxCitizenNameLength));
        service.AddSingleton<IPlayerCommandRepository>(_ => new PlayerCommands(playerContainer));
        service.AddSingleton<ICrewDisbandRepository>(_ =>
            new CrewDisbandRepository(crewContainer, disbandedCrewsContainer));
    }
}