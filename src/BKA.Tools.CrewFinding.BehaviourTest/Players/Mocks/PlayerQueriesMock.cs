using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.BehaviourTest.Players.Mocks;

public class PlayerQueriesMock : IPlayerQueries
{
    public PlayerQueriesMock(string expectedPlayerId, string playerName)
    {
        Players = new List<Player>
        {
            Player.Create(expectedPlayerId, playerName)
        };
    }

    public List<Player> Players { get; }

    public Task<Player?> GetPlayer(string playerId)
    {
        return Task.FromResult(Players.FirstOrDefault(p => p.Id == playerId));
    }
}