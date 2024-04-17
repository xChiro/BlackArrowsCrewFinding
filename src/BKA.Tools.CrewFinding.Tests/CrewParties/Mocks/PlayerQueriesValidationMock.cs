using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;

public class PlayerQueriesValidationMock : IPlayerQueries
{
    private readonly string _expectedPlayerId;
    private readonly string _captainName;

    public PlayerQueriesValidationMock(string expectedPlayerId, string captainName)
    {
        _expectedPlayerId = expectedPlayerId;
        _captainName = captainName;
    }

    public Task<Player?> GetPlayer(string playerId)
    {
        return Task.FromResult(playerId == _expectedPlayerId ? Player.Create(playerId, _captainName) : null);
    }
}