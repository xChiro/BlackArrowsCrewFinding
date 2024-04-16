using BKA.Tools.CrewFinding.Ports;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public class PlayerQueriesMock : IPlayerQueries
{
    private readonly string _expectedPlayerId;
    private readonly string _playerName;

    public PlayerQueriesMock(string expectedPlayerId, string playerName)
    {
        _expectedPlayerId = expectedPlayerId;
        _playerName = playerName;
    }
    
    public Task<Player?> GetPlayer(string playerId)
    {
        return Task.FromResult(playerId == _expectedPlayerId ? new Player(playerId, _playerName) : null);
    }
}