using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties.Documents;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties;

public class CrewDisbandRepository(Container container, Container disbandedCrewsContainer) : ICrewDisbandRepository
{
    public async Task Disband(string crewId)
    {
        var currentCrewTask = container.ReadItemAsync<CrewDocument>(crewId, new PartitionKey(crewId));
        var currentCrew = await currentCrewTask;
        
        var createTask =
            disbandedCrewsContainer.CreateItemAsync(currentCrew.Resource, new PartitionKey(currentCrew.Resource.Id));
        var deleteTask = container.DeleteItemAsync<CrewDocument>(crewId, new PartitionKey(crewId));

        await Task.WhenAll(createTask, deleteTask);
    }
}