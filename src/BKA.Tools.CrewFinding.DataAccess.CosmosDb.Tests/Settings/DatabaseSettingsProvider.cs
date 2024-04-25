using System.Text.Json;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase;
using BKA.Tools.CrewFinding.KeyVault;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;

public class DatabaseSettingsProvider : IDatabaseSettingsProvider<Container>
{
    private readonly IConfigurationRoot _configurationRoot;
    private readonly IKeySecretProvider _keySecretProvider;

    public DatabaseSettingsProvider(IKeySecretProvider keySecretProvider, IConfigurationRoot configurationRoot)
    {
        _keySecretProvider = keySecretProvider;
        _configurationRoot = configurationRoot;
    }

    public Container GetCrewContainer() => BuildContainer(GetCrewPartiesContainerName()).GetAwaiter().GetResult();
    public Container GetPlayerContainer() => BuildContainer(GetPlayerContainerName()).GetAwaiter().GetResult();

    private async Task<Container> BuildContainer(string containerId)
    {
        var cosmosClient = await CreateCosmosClient();
        var database = GetDatabase(cosmosClient);
        
        return database.GetContainer(containerId);
    }

    private async Task<CosmosClient> CreateCosmosClient()
    {
        var primaryKey = await _keySecretProvider.GetSecret(GetAzureKeyName());
        var serializerOptions = new CustomCosmosSerializer(new JsonSerializerOptions
            {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

        return new CosmosClientBuilder(GetCosmosDbEndpoint(), primaryKey)
            .WithCustomSerializer(serializerOptions)
            .Build();
    }

    private Database GetDatabase(CosmosClient cosmosClient)
    {
        return cosmosClient.GetDatabase(GetCosmosDbDatabase());
    }

    private string GetAzureKeyName()
    {
        return _configurationRoot["keyVault:azureKeyName"] ?? string.Empty;
    }

    private string GetCosmosDbEndpoint()
    {
        return _configurationRoot["cosmosDB:endpoint"] ?? string.Empty;
    }

    private string GetCosmosDbDatabase()
    {
        return _configurationRoot["cosmosDB:database"] ?? string.Empty;
    }

    private string GetCrewPartiesContainerName()
    {
        return _configurationRoot["cosmosDB:crewContainer"] ?? string.Empty;
    }
    
    private string GetPlayerContainerName()
    {
        return _configurationRoot["cosmosDB:playerContainer"] ?? string.Empty;
    }
}