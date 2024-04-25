using System.Text.Json;
using Microsoft.Azure.Cosmos.Fluent;

namespace BKA.Tools.CrewFinding.Azure.DataBase;

public class ContainerBuilder
{
    private readonly string _endPoint;
    private readonly string _key;

    public ContainerBuilder(string endPoint, string key)
    {
        _endPoint = endPoint;
        _key = key;
    }
    
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

        return new CosmosClientBuilder(_endPoint, _key)
            .WithCustomSerializer(serializerOptions)
            .Build();
    }
}