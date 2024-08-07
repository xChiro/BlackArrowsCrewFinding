using BKA.Tools.CrewFinding.Crews.Commands.Disbands;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Crews;

public record CrewDisbandmentResponse : ICrewDisbandmentResponse
{
    public string CrewId { get; private set; } = string.Empty;

    public void SetResult(string crewId)
    {
        CrewId = crewId;
    }
}