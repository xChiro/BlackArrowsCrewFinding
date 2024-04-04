using BKA.Tools.CrewFinding.CrewParties;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using Microsoft.Azure.Cosmos;

namespace BKA.Tools.CrewFinding.Azure.DataBase.CrewParties;

public class CrewPartyCommands(Container container) : ICrewPartyCommands
{
    public Task CreateCrewParty(CrewParty crewParty)
    {
        container.CreateItemAsync(crewParty, new PartitionKey(crewParty.Id));
        return Task.CompletedTask;
    }

    public Task AddPlayerToCrewParty(string playerId, string crewPartyId)
    {
        throw new NotImplementedException();
    }
}