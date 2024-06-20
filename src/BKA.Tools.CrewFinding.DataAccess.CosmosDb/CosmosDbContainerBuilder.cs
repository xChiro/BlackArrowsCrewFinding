using System.Text.Json;
using Microsoft.Azure.Cosmos.Fluent;

namespace BKA.Tools.CrewFinding.Azure.DataBase;

public class CosmosDbContainerBuilder(string endPoint, string key)
{
    public Container Build(string databaseId, string containerId)
    {
        var cosmosClient = CreateCosmosClient();
        var database = cosmosClient.GetDatabase(databaseId);
        
        return database.GetContainer(containerId);
    }
    
    private CosmosClient CreateCosmosClient()
    {
        var serializerOptions = new CustomCosmosSerializer(new JsonSerializerOptions
            {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

        return new CosmosClientBuilder(endPoint, key)
            .WithCustomSerializer(serializerOptions)
            .Build();
    }
}