using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.BehaviourTest.Players.Mocks;

public class PlayerCommandRepositoryMock : IPlayerCommandRepository
{
    public Player? StoredPlayer { get; set; }
    
    public Task Create(Player player)
    {
        StoredPlayer = player;
        return Task.CompletedTask;
    }

    public Task UpdateName(string playerId, string newName)
    {
        throw new NotImplementedException();
    }
}