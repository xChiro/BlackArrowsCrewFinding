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
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace BKA.Tools.CrewFinding.API.Functions;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var containerBuilder = CreateContainerBuilder();

        AddRepositories(builder, containerBuilder);
        AddServices(builder);
    }

    private static ContainerBuilder CreateContainerBuilder()
    {
        var keySecretsProvider = new KeySecretProviderBuilder(GetEnvironmentVariable("keyVaultEndpoint")).Build();
        var azureKey = keySecretsProvider.GetSecret(GetEnvironmentVariable("keyVaultAzureKeyName"));

        return new ContainerBuilder(GetEnvironmentVariable("cosmosDBEndpoint"), azureKey);
    }

    private static void AddRepositories(IFunctionsHostBuilder builder, ContainerBuilder containerBuilder)
    {
        var databaseId = GetEnvironmentVariable("cosmosDBDatabase");
        var crewContainer = containerBuilder.Build(databaseId, GetEnvironmentVariable("cosmosDBCrewContainer"));
        var playerContainer = containerBuilder.Build(databaseId, GetEnvironmentVariable("cosmosDBPlayerContainer"));

        builder.Services.AddScoped<ICrewCommandRepository>(_ => new CrewCommandRepository(crewContainer));
        builder.Services.AddScoped<ICrewQueryRepository>(_ => new CrewQueryRepository(crewContainer));
        builder.Services.AddScoped<IPlayerQueryRepository>(_ => new PlayerQueries(playerContainer));
    }

    private static void AddServices(IFunctionsHostBuilder builder)
    {
        var maxCrewSize = Convert.ToInt32(GetEnvironmentVariable("maxCrewSize"));

        builder.Services.AddScoped<ICrewCreator>(
            serviceProvider =>
                new CrewCreator(
                    serviceProvider.GetRequiredService<ICrewCommandRepository>(),
                    serviceProvider.GetRequiredService<ICrewQueryRepository>(),
                    serviceProvider.GetRequiredService<IPlayerQueryRepository>(),
                    maxCrewSize));
    }

    private static string GetEnvironmentVariable(string name)
    {
        return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process) ?? string.Empty;
    }
}