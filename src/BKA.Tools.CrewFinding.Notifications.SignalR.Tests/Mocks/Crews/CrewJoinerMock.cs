using BKA.Tools.CrewFinding.Crews.Commands.JoinRequests;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.Crews;

public class CrewJoinerMock : ICrewJoiner
{
    public string CrewId { get; private set; } = string.Empty;

    public Task Join(string crewId)
    {
        CrewId = crewId;

        return Task.CompletedTask;
    }
}