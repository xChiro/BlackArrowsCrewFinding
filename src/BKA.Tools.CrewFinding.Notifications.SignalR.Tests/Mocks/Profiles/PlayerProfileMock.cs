using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Queries.PlayerProfiles;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.Profiles;

public class PlayerProfileMock(string playerId, string citizenName, string crewId, string crewName)
    : IProfileViewer
{
    public Task View(string playerId, IProfileResponse response)
    {
        response.SetResponse(Player.Create(playerId, citizenName, 2, 16), crewId, crewName);
        return Task.CompletedTask;
    }
}