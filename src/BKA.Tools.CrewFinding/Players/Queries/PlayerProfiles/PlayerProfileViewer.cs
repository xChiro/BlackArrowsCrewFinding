using BKA.Tools.CrewFinding.Players.Exceptions;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Players.Queries.PlayerProfiles;

public class PlayerProfileViewer(IPlayerQueryRepository playerQueryMock) : IPlayerProfileViewer
{
    public async Task<Player> View(string playerId)
    {
        var player = await playerQueryMock.GetPlayer(playerId);

        if (player is null)
        {
            throw new PlayerNotFoundException(playerId);
        }

        return player;
    }
}