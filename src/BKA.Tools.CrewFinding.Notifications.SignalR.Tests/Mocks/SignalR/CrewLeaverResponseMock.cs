using BKA.Tools.CrewFinding.Crews.Commands.Leave;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.SignalR;

public class CrewLeaverResponseMock : ICrewLeaverResponse
{
    public string CrewId { get; private set; }

    public void SetResponse(string crewId)
    {
        CrewId = crewId;
    }
}