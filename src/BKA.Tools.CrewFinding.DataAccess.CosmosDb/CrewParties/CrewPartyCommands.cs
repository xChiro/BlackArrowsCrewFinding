using BKA.Tools.CrewFinding.CrewParties;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Values;
using Microsoft.Azure.Cosmos;

namespace BKA.Tools.CrewFinding.Azure.DataBase.CrewParties;

public class CrewPartyCommands(Container container) : ICrewPartyCommands
{
    public async Task CreateCrewParty(CrewParty crewParty)
    {
        await container.CreateItemAsync(crewParty, new PartitionKey(crewParty.Id));
    }

    public Task UpdateMembers(string crewPartyId, IEnumerable<Player> crewPartyMembers)
    {
        throw new NotImplementedException();
    }
}