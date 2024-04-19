using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;

public class PlayerQueriesValidationMock : IPlayerQueries
{
    private readonly string _expectedPlayerId;
    private readonly string _playerName;
    private readonly bool _playerAlreadyInAParty;

    public PlayerQueriesValidationMock(string expectedPlayerId, string playerName, bool playerAlreadyInAParty = false)
    {
        _expectedPlayerId = expectedPlayerId;
        _playerName = playerName;
        _playerAlreadyInAParty = playerAlreadyInAParty;
    }

    public Task<bool> PlayerAlreadyInACrew(string captainId)
    {
        return Task.FromResult(_playerAlreadyInAParty);
    }

    public Task<Player?> GetPlayer(string playerId)
    {
        return Task.FromResult(playerId == _expectedPlayerId ? Player.Create(playerId, _playerName) : null);
    }
}