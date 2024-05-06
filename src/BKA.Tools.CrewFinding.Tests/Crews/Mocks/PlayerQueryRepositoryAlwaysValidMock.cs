using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks;

public class PlayerQueryRepositoryAlwaysValidMock(string captainName) : IPlayerQueryRepository
{
    public Task<Player?> GetPlayer(string playerId)
    {
        return Task.FromResult(Player.Create(playerId, captainName))!;
    }
}