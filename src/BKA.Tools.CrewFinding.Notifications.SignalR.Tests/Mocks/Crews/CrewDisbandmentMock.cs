using BKA.Tools.CrewFinding.Crews.Commands.Disbands;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.Crews;

public class CrewDisbandmentMock : ICrewDisbandment
{
    public Task Disband(ICrewDisbandmentResponse? output = null)
    {
        output?.SetResult("crewId");
        return Task.CompletedTask;
    }
}