using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Tests.Players;

public class PlayerCommandRepositoryMock : IPlayerCommandRepository
{
    public Player? StoredPlayer { get; private set; }

    public Task Create(Player player)
    {
        StoredPlayer = player;
        return Task.CompletedTask;
    }
}