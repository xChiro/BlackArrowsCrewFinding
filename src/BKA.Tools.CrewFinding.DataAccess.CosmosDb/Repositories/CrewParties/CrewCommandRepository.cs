using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties.Documents;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.Players.Documents;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties;

public class CrewCommandRepository : ICrewCommandRepository
{
    private readonly Container _container;

    public CrewCommandRepository(Container container)
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
            PatchOperation.Replace("/crew", members)
        };
        
        await _container.PatchItemAsync<CrewDocument>(crewPartyId, new PartitionKey(crewPartyId), patchOperations);
    }
}