using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties.Documents;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties;

public class CrewValidationRepository : ICrewValidationRepository, ICrewQueryRepository
{
    private readonly Container _container;

    public CrewValidationRepository(Container container)
    {
        _container = container;
    }

    public Task<bool> IsPlayerInActiveCrew(string playerId)
    {
        const string queryString =
            "SELECT c.id FROM c WHERE c.captainId = @playerId OR ARRAY_CONTAINS(c.crew, { 'id': @playerId }, true)";

        var queryDefinition = new QueryDefinition(queryString)
            .WithParameter("@playerId", playerId);

        var query = _container.GetItemQueryIterator<CrewDocument>(queryDefinition);

        return query.ReadNextAsync().ContinueWith(task => task.Result.Any());
    }

    public async Task<Crew?> GetCrew(string crewId)
    {
        try
        {
            var itemResponseAsync = await _container.ReadItemAsync<CrewDocument>(crewId, new PartitionKey(crewId));

            return itemResponseAsync.StatusCode == System.Net.HttpStatusCode.NotFound
                ? null : itemResponseAsync.Resource.ToCrew();
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public Task<Crew[]> GetCrews(DateTime from, DateTime to)
    {
        const string queryString = "SELECT * FROM c WHERE c.createdAt >= @from AND c.createdAt <= @to";
        
        var queryDefinition = new QueryDefinition(queryString)
            .WithParameter("@from", from)
            .WithParameter("@to", to);
        
        var query = _container.GetItemQueryIterator<CrewDocument>(queryDefinition);
        
        return query.ReadNextAsync().ContinueWith(task => task.Result.Select(c => c.ToCrew()).ToArray());
    }

    public Task<bool> DoesUserOwnAnActiveCrew(string userId)
    {
        const string queryString = "SELECT c.id FROM c WHERE c.captainId = @userId";

        var queryDefinition = new QueryDefinition(queryString)
            .WithParameter("@userId", userId);

        var query = _container.GetItemQueryIterator<CrewDocument>(queryDefinition);

        return query.ReadNextAsync().ContinueWith(task => task.Result.Count != 0);
    }
}