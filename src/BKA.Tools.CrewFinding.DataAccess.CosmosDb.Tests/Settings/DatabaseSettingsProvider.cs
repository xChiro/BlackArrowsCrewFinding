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

    public Container GetContainer() => GetContainerCore().GetAwaiter().GetResult();

    private async Task<Container> GetContainerCore()
    {
        var primaryKey = await _keySecretsProvider.GetSecret(_configurationRoot["azureKeyName"] ?? string.Empty);

        var cosmosClient = CreateCosmosClient(primaryKey);

        var database = cosmosClient.GetDatabase(_configurationRoot["cosmosDB:database"]);
        return database.GetContainer(_configurationRoot["cosmosDB:container"]);
    }

    private CosmosClient CreateCosmosClient(string primaryKey)
    {
        var serializerOptions = new CustomCosmosSerializer(new JsonSerializerOptions
            {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

        return new CosmosClientBuilder(_configurationRoot["cosmosDB:endpoint"], primaryKey)
            .WithCustomSerializer(serializerOptions)
            .Build();
    }
}