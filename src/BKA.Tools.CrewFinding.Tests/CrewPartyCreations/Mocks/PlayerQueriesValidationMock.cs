using System.Threading.Tasks;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.CrewPartyCreations.Mocks;

public class PlayerQueriesValidationMock(string expectedPlayerId, string captainName) : IPlayerQueries
{
    public Task<Player?> GetPlayer(string playerId)
    {
        return Task.FromResult(playerId == expectedPlayerId ? new Player(playerId, captainName) : null);
    }
}