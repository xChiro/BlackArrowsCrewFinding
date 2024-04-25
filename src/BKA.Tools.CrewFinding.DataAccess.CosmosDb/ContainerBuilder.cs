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
        var cosmosClient = new CosmosClient(_endPoint, _key);
        var database = cosmosClient.GetDatabase(databaseId);
        
        return database.GetContainer(containerId);
    }
}