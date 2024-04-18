using System;
using System.Text.Json;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;

public class DatabaseSettingsProvider : IDatabaseSettingsProvider<Container>
{
    private readonly IConfigurationRoot _configurationRoot;
    private readonly IKeySecretsProvider _keySecretsProvider;

    public DatabaseSettingsProvider(IKeySecretsProvider keySecretsProvider, IConfigurationRoot configurationRoot)
    {
        _keySecretsProvider = keySecretsProvider ?? throw new ArgumentNullException(nameof(keySecretsProvider));
        _configurationRoot = configurationRoot ?? throw new ArgumentNullException(nameof(configurationRoot));
    }

    public Container GetCrewPartyContainer() => BuildCrewPartyContainer().GetAwaiter().GetResult();
    public Container GetPlayerContainer() => BuildPlayerContainer().GetAwaiter().GetResult();

    private async Task<Container> BuildPlayerContainer()
    {
        var cosmosClient = await CreateCosmosClient();
        var database = GetDatabase(cosmosClient);
        
        return database.GetContainer(GetPlayerContainerName());
    }

    private async Task<Container> BuildCrewPartyContainer()
    {
        var cosmosClient = await CreateCosmosClient();
        var database = GetDatabase(cosmosClient);
        
        return database.GetContainer(GetCrewPartiesContainerName());
    }

    private async Task<CosmosClient> CreateCosmosClient()
    {
        var primaryKey = await _keySecretsProvider.GetSecret(GetAzureKeyName());
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
        return _configurationRoot["azureKeyName"] ?? string.Empty;
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
        return _configurationRoot["cosmosDB:crewPartyContainer"] ?? string.Empty;
    }
    
    private string GetPlayerContainerName()
    {
        return _configurationRoot["cosmosDB:playerContainer"] ?? string.Empty;
    }
}