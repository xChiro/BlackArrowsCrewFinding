using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Queries.PlayerProfiles;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.Profiles;

public class ProfileResponseMock : IProfileResponse
{
    public Player? Player { get; private set; }
    public string ActiveCrewName { get; private set; } = string.Empty;
    public string ActiveCrewId { get; private set; } = string.Empty;

    public void SetResponse(Player player)
    {
        Player = player;
    }

    public void SetResponse(Player player, string activeCrewId, string activeCrewName)
    {
        Player = player;
        ActiveCrewId = activeCrewId;
        ActiveCrewName = activeCrewName;
    }
}