using System.Threading.Tasks;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.CrewPartyCreations.Mocks;

public class PlayerQueriesAlwaysValidMock(string captainName) : IPlayerQueries
{
    
    public Task<Player?> GetPlayer(string playerId)
    {
        return Task.FromResult(new Player(playerId, captainName));
    }
}