using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties.Documents;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;
using Microsoft.Azure.Cosmos.Linq;

namespace BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties;

public class CrewQueries : ICrewQueries
{
    private readonly Container _container;

    public CrewQueries(Container container)
    {
        _container = container;
    }
    
    public async Task<Crew?> GetCrewParty(string crewPartyId)
    {
        var crewDocument = (await _container.GetItemLinqQueryable<CrewDocument>()
            .Where(c => c.Id == crewPartyId)
            .ToFeedIterator()
            .ReadNextAsync())
            .FirstOrDefault();
        
        return crewDocument?.ToCrew();
    }
}