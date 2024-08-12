using BKA.Tools.CrewFinding.Crews.Commands.Creators;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.Crews;

public class CrewCreatorMock(string id, string crewName) : ICrewCreator
{
    public Task Create(ICrewCreatorRequest request, ICrewCreatorResponse output)
    {
        output.SetResponse(id, crewName);
        return Task.CompletedTask;
    }
}