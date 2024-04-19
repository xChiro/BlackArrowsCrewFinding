using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.BehaviourTest.Players.Mocks;

public class PlayerQueriesMock : IPlayerQueries
{
    private readonly bool _playerAlreadyOwnsAParty;
    
    public PlayerQueriesMock(string expectedPlayerId, string playerName, bool playerAlreadyOwnsAParty = false)
    {
        _playerAlreadyOwnsAParty = playerAlreadyOwnsAParty;
        Players = new List<Player>
        {
            Player.Create(expectedPlayerId, playerName)
        };
    }

    public List<Player> Players { get; }
    
    public Task<bool> PlayerAlreadyInACrew(string captainId)
    {
        return Task.FromResult(_playerAlreadyOwnsAParty);
    }

    public Task<Player?> GetPlayer(string playerId)
    {
        return Task.FromResult(Players.FirstOrDefault(p => p.Id == playerId));
    }
}