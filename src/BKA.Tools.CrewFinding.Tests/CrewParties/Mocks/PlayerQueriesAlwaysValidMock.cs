using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;

public class PlayerQueriesAlwaysValidMock : IPlayerQueries
{
    private readonly string _captainName;

    public PlayerQueriesAlwaysValidMock(string captainName)
    {
        _captainName = captainName;
    }

    public Task<Player?> GetPlayer(string playerId)
    {
        return Task.FromResult(Player.Create(playerId, _captainName));
    }
}