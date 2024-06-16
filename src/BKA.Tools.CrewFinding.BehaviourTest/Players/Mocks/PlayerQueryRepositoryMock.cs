using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.BehaviourTest.Players.Mocks;

public class PlayerQueryRepositoryMock(string expectedPlayerId, string playerName) : IPlayerQueryRepository
{
    public List<Player> Players { get; } = [Player.Create(expectedPlayerId, playerName, 2, 16)];

    public Task<Player?> GetPlayer(string playerId)
    {
        return Task.FromResult(Players.FirstOrDefault(p => p.Id == playerId));
    }
}