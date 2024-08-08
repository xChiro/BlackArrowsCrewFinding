using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Queries.PlayerProfiles;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Profiles;

public class ProfileViewerResponse : IProfileResponse
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