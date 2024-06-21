using BKA.Tools.CrewFinding.Azure.DataBase.CrewParties.Documents;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Azure.DataBase.CrewParties;

public class CrewDisbandRepository(Container crewContainer, Container disbandedCrewsContainer) : ICrewDisbandRepository
{
    public async Task Disband(string crewId)
    {
        var currentCrewTask = crewContainer.ReadItemAsync<CrewDocument>(crewId, new PartitionKey(crewId));
        var currentCrew = await currentCrewTask;

        var createTask =
            disbandedCrewsContainer.CreateItemAsync(currentCrew.Resource, new PartitionKey(currentCrew.Resource.Id));
        var deleteTask = crewContainer.DeleteItemAsync<CrewDocument>(crewId, new PartitionKey(crewId));

        await Task.WhenAll(createTask, deleteTask);
    }

    public async Task Disband(string[] crewIds)
    {
        var tasks = crewIds.Select(Disband).ToArray();
        await Task.WhenAll(tasks);
    }
}