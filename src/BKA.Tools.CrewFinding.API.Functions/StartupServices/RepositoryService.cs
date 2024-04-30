using BKA.Tools.CrewFinding.Azure.DataBase;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.Players;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace BKA.Tools.CrewFinding.API.Functions.StartupServices;

public class RepositoryService
{
    public static void AddRepositories(IServiceCollection service, ContainerBuilder containerBuilder)
    {
        var databaseId = Configuration.GetEnvironmentVariable("cosmosDBDatabase");
        var crewContainer =
            containerBuilder.Build(databaseId, Configuration.GetEnvironmentVariable("cosmosDBCrewContainer"));
        var playerContainer =
            containerBuilder.Build(databaseId, Configuration.GetEnvironmentVariable("cosmosDBPlayerContainer"));

        service.AddSingleton<ICrewCommandRepository>(_ => new CrewCommandRepository(crewContainer));
        service.AddSingleton<ICrewQueryRepository>(_ => new CrewValidationRepository(crewContainer));
        service.AddSingleton<ICrewValidationRepository>(_ => new CrewValidationRepository(crewContainer));
        service.AddSingleton<IPlayerQueryRepository>(_ => new PlayerQueries(playerContainer));
        service.AddSingleton<IPlayerCommandRepository>(_ => new PlayerCommands(playerContainer));
    }
}