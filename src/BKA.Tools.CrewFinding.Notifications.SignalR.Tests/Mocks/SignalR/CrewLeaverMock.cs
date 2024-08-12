using BKA.Tools.CrewFinding.Crews.Commands.Leave;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.SignalR;

public class CrewLeaverMock(string crewId = "") : ICrewLeaver
{
    public int LeaveCallCount { get; private set; }

    public Task Leave(ICrewLeaverResponse output)
    {
        LeaveCallCount++;
        output.SetResponse(crewId);
        return Task.CompletedTask;
    }
}