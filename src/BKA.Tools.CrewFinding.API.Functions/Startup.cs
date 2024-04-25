using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.API.Functions;
using BKA.Tools.CrewFinding.Azure.DataBase;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.Players;
using BKA.Tools.CrewFinding.Crews.CreateRequests;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.KeyVault;
using BKA.Tools.CrewFinding.Players.Ports;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace BKA.Tools.CrewFinding.API.Functions;

public class Startup : FunctionsStartup
{
    public override async void Configure(IFunctionsHostBuilder builder)
    {
        var config = builder.GetContext().Configuration;
        var containerBuilder = await CreateContainerBuilder(config);

        AddRepositories(builder, containerBuilder, config);
        AddServices(builder, config);
    }

    private static async Task<ContainerBuilder> CreateContainerBuilder(IConfiguration config)
    {
        var keySecretsProvider = new KeySecretProviderBuilder(config["KeyVault:endpoint"]).Build();
        var azureKey = await keySecretsProvider.GetSecret(config["KeyVault:azureKeyName"]);

        return new ContainerBuilder(config["cosmosDB:endpoint"], azureKey);
    }

    private static void AddRepositories(IFunctionsHostBuilder builder, ContainerBuilder containerBuilder,
        IConfiguration config)
    {
        var databaseId = config["cosmosDB:database"];
        var crewContainer = containerBuilder.Build(databaseId, config["cosmosDB:crewContainer"]);
        var playerContainer = containerBuilder.Build(databaseId, config["cosmosDB:playerContainer"]);

        builder.Services.AddScoped<ICrewCommandRepository>(_ => new CrewCommandRepository(crewContainer));
        builder.Services.AddScoped<ICrewQueryRepository>(_ => new CrewQueryRepository(crewContainer));
        builder.Services.AddScoped<IPlayerQueryRepository>(_ => new PlayerQueries(playerContainer));
    }

    private static void AddServices(IFunctionsHostBuilder builder, IConfiguration config)
    {
        var maxCrewSize = Convert.ToInt32(config["maxCrewSize"]);

        builder.Services.AddScoped<ICrewCreator>(
            serviceProvider =>
                new CrewCreator(
                    serviceProvider.GetService<ICrewCommandRepository>(),
                    serviceProvider.GetService<ICrewQueryRepository>(),
                    serviceProvider.GetService<IPlayerQueryRepository>(),
                    maxCrewSize));
    }
}