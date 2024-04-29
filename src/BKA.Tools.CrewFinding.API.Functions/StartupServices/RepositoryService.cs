using BKA.Tools.CrewFinding.Azure.DataBase;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.Players;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players.Ports;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace BKA.Tools.CrewFinding.API.Functions.StartupServices;

public class RepositoryService
{
    public static void AddRepositories(IFunctionsHostBuilder builder, ContainerBuilder containerBuilder)
    {
        var databaseId = Configuration.GetEnvironmentVariable("cosmosDBDatabase");
        var crewContainer = containerBuilder.Build(databaseId, Configuration.GetEnvironmentVariable("cosmosDBCrewContainer"));
        var playerContainer = containerBuilder.Build(databaseId, Configuration.GetEnvironmentVariable("cosmosDBPlayerContainer"));

        builder.Services.AddSingleton<ICrewCommandRepository>(_ => new CrewCommandRepository(crewContainer));
        builder.Services.AddSingleton<ICrewValidationRepository>(_ => new CrewValidationRepository(crewContainer));
        builder.Services.AddSingleton<IPlayerQueryRepository>(_ => new PlayerQueries(playerContainer));
        builder.Services.AddSingleton<IPlayerCommandRepository>(_ => new PlayerCommands(playerContainer));
    }
}