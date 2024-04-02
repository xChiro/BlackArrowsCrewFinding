using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Ports;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public class CrewPartyQueriesMock : ICrewPartyQueries
{
    private readonly bool _hasCreatedParty;

    public CrewPartyQueriesMock(bool hasCreatedParty)
    {
        _hasCreatedParty = hasCreatedParty;
    }
    
    public Task<bool> PlayerHasCreatedParty(string captainId)
    {
        return Task.FromResult(_hasCreatedParty);
    }
}