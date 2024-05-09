using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Queries.PlayerProfiles;

namespace BKA.Tools.CrewFinding.Tests.Players.Mocks;

public class PlayerProfileResponseMock : IPlayerProfileResponse
{
    public Player? Player { get; set; }
    public string ActiveCrewId { get; set; } = string.Empty;
    public string ActiveCrewName { get; set; } = string.Empty;

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