using System.Text.Json;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase;
using BKA.Tools.CrewFinding.KeyVault;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;

public class DatabaseSettingsProvider(IKeySecretProvider keySecretProvider, IConfigurationRoot configurationRoot)
    : IDatabaseSettingsProvider<Container>
{
    public Container GetCrewContainer() => BuildContainer(GetCrewPartiesContainerName()).GetAwaiter().GetResult();
    public Container GetPlayerContainer() => BuildContainer(GetPlayerContainerName()).GetAwaiter().GetResult();
    public Container GetDisbandedCrewsContainer() => BuildContainer(GetDisbandedCrewsContainerName()).GetAwaiter().GetResult();
    public Container GetVoiceChannelContainer() => BuildContainer(GetVoiceChannelContainerName()).GetAwaiter().GetResult();

    private async Task<Container> BuildContainer(string containerId)
    {
        var cosmosClient = await CreateCosmosClient();
        var database = GetDatabase(cosmosClient);
        
        return database.GetContainer(containerId);
    }

    private Task<CosmosClient> CreateCosmosClient()
    {
        var primaryKey = keySecretProvider.GetSecret(GetAzureKeyName());
        var serializerOptions = new CustomCosmosSerializer(new JsonSerializerOptions
            {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

        return Task.FromResult(new CosmosClientBuilder(GetCosmosDbEndpoint(), primaryKey)
            .WithCustomSerializer(serializerOptions)
            .Build());
    }

    private Database GetDatabase(CosmosClient cosmosClient)
    {
        return cosmosClient.GetDatabase(GetCosmosDbDatabase());
    }

    private string GetAzureKeyName()
    {
        return configurationRoot["keyVault:azureKeyName"] ?? string.Empty;
    }

    private string GetCosmosDbEndpoint()
    {
        return configurationRoot["cosmosDB:endpoint"] ?? string.Empty;
    }

    private string GetCosmosDbDatabase()
    {
        return configurationRoot["cosmosDB:database"] ?? string.Empty;
    }

    private string GetCrewPartiesContainerName()
    {
        return configurationRoot["cosmosDB:crewContainer"] ?? string.Empty;
    }

    private string GetDisbandedCrewsContainerName()
    {
        return configurationRoot["cosmosDB:disbandedCrewsContainer"] ?? string.Empty;
    }

    private string GetPlayerContainerName()
    {
        return configurationRoot["cosmosDB:playerContainer"] ?? string.Empty;
    }

    private string GetVoiceChannelContainerName()
    {
        return configurationRoot["cosmosDB:voiceChannelContainer"] ?? string.Empty;
    }
}