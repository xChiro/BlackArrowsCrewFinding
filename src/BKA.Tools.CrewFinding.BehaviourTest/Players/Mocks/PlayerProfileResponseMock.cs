using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Queries.PlayerProfiles;

namespace BKA.Tools.CrewFinding.BehaviourTest.Players.Mocks;

public class PlayerProfileResponseMock(PlayerResultsContext playerResultsContext) : IPlayerProfileResponse
{
    public void SetResponse(Player player)
    {
        playerResultsContext.Player = player;
    }

    public void SetResponse(Player player, string activeCrewId, string activeCrewName)
    {
        playerResultsContext.Player = player;
        playerResultsContext.ActiveCrewId = activeCrewId;
        playerResultsContext.ActiveCrewName = activeCrewName;
    }
}