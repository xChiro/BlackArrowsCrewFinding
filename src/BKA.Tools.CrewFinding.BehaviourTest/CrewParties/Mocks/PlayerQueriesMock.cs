using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public class PlayerQueriesMock : IPlayerQueries
{
    public Task<Player?> GetPlayer(string playerId)
    {
        throw new NotImplementedException();
    }
}