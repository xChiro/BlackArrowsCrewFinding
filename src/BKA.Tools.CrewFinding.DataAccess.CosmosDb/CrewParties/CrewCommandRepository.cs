using BKA.Tools.CrewFinding.Azure.DataBase.CrewParties.Documents;
using BKA.Tools.CrewFinding.Azure.DataBase.Players.Documents;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.Azure.DataBase.CrewParties;

public class CrewCommandRepository(Container crewContainer, Container disbandCrewContainer) : ICrewCommandRepository
{
    public async Task CreateCrew(Crew crew)
    {
        var document = CrewDocument.CreateFromCrew(crew);
        await crewContainer.CreateItemAsync(document, new PartitionKey(document.Id));
    }

    public async Task UpdateMembers(string crewId, IEnumerable<Player> crewMembers)
    {
        var members = crewMembers.Select(PlayerDocument.CreateFromPlayer).ToList();

        var patchOperations = new List<PatchOperation>
        {
            PatchOperation.Replace("/crew", members)
        };

        await crewContainer.PatchItemAsync<CrewDocument>(crewId, new PartitionKey(crewId), patchOperations);
    }

    public async Task DeletePlayerHistory(string playerId)
    {
        var queryDefinition =
            new QueryDefinition(
                    "SELECT * FROM c WHERE c.captainId = @playerId   OR ARRAY_CONTAINS(c.crew, {'id': @playerId}, true)")
                .WithParameter("@playerId", playerId);
        var resultSetIterator = disbandCrewContainer.GetItemQueryIterator<CrewDocument>(queryDefinition);

        var deleteTasks = new List<Task>();

        while (resultSetIterator.HasMoreResults)
        {
            var response = await resultSetIterator.ReadNextAsync();

            deleteTasks.AddRange(response.Select(crewDocument =>
                disbandCrewContainer.DeleteItemAsync<CrewDocument>(crewDocument.Id,
                    new PartitionKey(crewDocument.Id))));
        }

        await Task.WhenAll(deleteTasks);
    }

    public Task DeleteCrew(string crewId)
    {
        throw new NotImplementedException();
    }
}