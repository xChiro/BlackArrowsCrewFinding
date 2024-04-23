using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;

public class PlayerQueryRepositoryValidationMock : IPlayerQueryRepository
{
    private readonly string _expectedPlayerId;
    private readonly string _playerName;

    public PlayerQueryRepositoryValidationMock(string expectedPlayerId, string playerName)
    {
        _expectedPlayerId = expectedPlayerId;
        _playerName = playerName;
    }

    public Task<Player?> GetPlayer(string playerId)
    {
        return Task.FromResult(playerId == _expectedPlayerId ? Player.Create(playerId, _playerName) : null);
    }
}