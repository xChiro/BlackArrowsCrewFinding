using BKA.Tools.CrewFinding.Azure.DataBase.CrewParties.Documents;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Azure.DataBase.CrewParties;

public class CrewQueryRepository(Container container, int minNameLength, int maxNameLength) : ICrewValidationRepository, ICrewQueryRepository
{
    public Task<bool> IsPlayerInActiveCrew(string playerId)
    {
        const string queryString =
            "SELECT c.id FROM c WHERE c.captainId = @playerId OR ARRAY_CONTAINS(c.crew, { 'id': @playerId }, true)";

        var queryDefinition = new QueryDefinition(queryString)
            .WithParameter("@playerId", playerId);

        var query = container.GetItemQueryIterator<CrewDocument>(queryDefinition);

        return query.ReadNextAsync().ContinueWith(task => task.Result.Any());
    }

    public Task<Crew[]> GetCrewsExpiredByDate(DateTime expiryDate)
    {
        const string queryString = "SELECT * FROM c WHERE c.createdAt <= @expiryDate";

        var queryDefinition = new QueryDefinition(queryString)
            .WithParameter("@expiryDate", expiryDate);

        var query = container.GetItemQueryIterator<CrewDocument>(queryDefinition);

        return query.ReadNextAsync().ContinueWith(task => task.Result.Select(c => c.ToCrew(minNameLength, maxNameLength)).ToArray());
    }

    public Task<Crew?> GetActiveCrewByPlayerId(string playerId)
    {
        const string queryString =
            "SELECT * FROM c WHERE c.captainId = @playerId OR ARRAY_CONTAINS(c.crew, { 'id': @playerId }, true)";

        var queryDefinition = new QueryDefinition(queryString)
            .WithParameter("@playerId", playerId);

        var query = container.GetItemQueryIterator<CrewDocument>(queryDefinition);

        return query.ReadNextAsync().ContinueWith(task => task.Result.FirstOrDefault()?.ToCrew(minNameLength, maxNameLength));
    }

    public async Task<Crew?> GetCrew(string crewId)
    {
        try
        {
            var itemResponseAsync = await container.ReadItemAsync<CrewDocument>(crewId, new PartitionKey(crewId));

            return itemResponseAsync.StatusCode == System.Net.HttpStatusCode.NotFound
                ? null
                : itemResponseAsync.Resource.ToCrew(minNameLength, maxNameLength);
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

        var query = container.GetItemQueryIterator<CrewDocument>(queryDefinition);

        return query.ReadNextAsync().ContinueWith(task => task.Result.Select(c => c.ToCrew(minNameLength, maxNameLength)).ToArray());
    }
}