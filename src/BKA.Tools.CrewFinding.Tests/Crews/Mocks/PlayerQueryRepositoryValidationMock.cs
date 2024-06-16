using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks;

public class PlayerQueryRepositoryValidationMock(
    string expectedPlayerId,
    string playerName,
    int minLength = 2,
    int maxLength = 30) : IPlayerQueryRepository
{
    public Task<Player?> GetPlayer(string playerId)
    {
        return Task.FromResult(playerId == expectedPlayerId
            ? Player.Create(playerId, playerName, minLength, maxLength)
            : null);
    }
}