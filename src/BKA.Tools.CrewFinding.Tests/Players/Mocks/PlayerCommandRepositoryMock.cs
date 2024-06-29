using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Tests.Players.Mocks;

public class PlayerCommandRepositoryMock : IPlayerCommandRepository
{
    public Player? StoredPlayer { get; private set; }
    public string NewName { get; set; } = string.Empty;
    public string DeletedPlayerId { get; private set; } = string.Empty;

    public Task Create(Player player)
    {
        StoredPlayer = player;
        return Task.CompletedTask;
    }

    public Task UpdateName(string playerId, string newName)
    {
        NewName = newName;
        return Task.CompletedTask;
    }

    public Task Delete(string playerId)
    {
        DeletedPlayerId = playerId;
        return Task.CompletedTask;
    }
}