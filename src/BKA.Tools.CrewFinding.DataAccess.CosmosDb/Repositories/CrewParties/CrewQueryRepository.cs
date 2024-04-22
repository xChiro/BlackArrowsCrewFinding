using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties.Documents;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties;

public class CrewQueryRepository : ICrewQueryRepository
{
    private readonly Container _container;

    public CrewQueryRepository(Container container)
    {
        _container = container;
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
}