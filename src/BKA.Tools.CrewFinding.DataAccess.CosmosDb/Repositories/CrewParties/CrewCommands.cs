using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties.Documents;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.Players.Documents;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players;
using Microsoft.Azure.Cosmos;

namespace BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties;

public class CrewCommands : ICrewCommands
{
    private readonly Container _container;

    public CrewCommands(Container container)
    {
        _container = container;
    }

    public async Task CreateCrew(Crew crew)
    {
        var document = CrewDocument.CreateFromCrew(crew);
        await _container.CreateItemAsync(document, new PartitionKey(document.Id));
    }

    public async Task UpdateMembers(string crewPartyId, IEnumerable<Player> crewPartyMembers)
    {
        var members = crewPartyMembers.Select(PlayerDocument.CreateFromPlayer).ToList();
        
        var patchOperations = new List<PatchOperation>
        {
            PatchOperation.Replace("/members", members)
        };
        
        await _container.PatchItemAsync<CrewDocument>(crewPartyId, new PartitionKey(crewPartyId), patchOperations);
    }
}