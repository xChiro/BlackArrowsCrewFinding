using BKA.Tools.CrewFinding.Azure.DataBase.Players;
using BKA.Tools.CrewFinding.CrewParties;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Players;
using Microsoft.Azure.Cosmos;

namespace BKA.Tools.CrewFinding.Azure.DataBase.CrewParties;

public class CrewPartyCommands : ICrewPartyCommands
{
    private readonly Container _container;

    public CrewPartyCommands(Container container)
    {
        _container = container;
    }

    public async Task CreateCrewParty(CrewParty crewParty)
    {
        var document = CrewPartyDocument.CreateFromCrewParty(crewParty);
        await _container.CreateItemAsync(document, new PartitionKey(document.Id));
    }

    public async Task UpdateMembers(string crewPartyId, IEnumerable<Player> crewPartyMembers)
    {
        var members = crewPartyMembers.Select(PlayerDocument.CreateFromPlayer).ToList();
        
        var patchOperations = new List<PatchOperation>
        {
            PatchOperation.Replace("/members", members)
        };
        
        await _container.PatchItemAsync<CrewPartyDocument>(crewPartyId, new PartitionKey(crewPartyId), patchOperations);
    }
}