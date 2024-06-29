using BKA.Tools.CrewFinding.Azure.DataBase.CrewParties.Documents;
using BKA.Tools.CrewFinding.Azure.DataBase.Players.Documents;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.Azure.DataBase.CrewParties;

public class CrewCommandRepository(Container container) : ICrewCommandRepository
{
    public async Task CreateCrew(Crew crew)
    {
        var document = CrewDocument.CreateFromCrew(crew);
        await container.CreateItemAsync(document, new PartitionKey(document.Id));
    }

    public async Task UpdateMembers(string crewId, IEnumerable<Player> crewMembers)
    {
        var members = crewMembers.Select(PlayerDocument.CreateFromPlayer).ToList();

        var patchOperations = new List<PatchOperation>
        {
            PatchOperation.Replace("/crew", members)
        };

        await container.PatchItemAsync<CrewDocument>(crewId, new PartitionKey(crewId), patchOperations);
    }

    public Task DeletePlayerHistory(string playerId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCrew(string crewId)
    {
        throw new NotImplementedException();
    }
}