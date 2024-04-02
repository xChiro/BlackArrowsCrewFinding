using BKA.Tools.CrewFinding.CrewParties;
using BKA.Tools.CrewFinding.CrewParties.Ports;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public class CrewPartyQueriesMock : ICrewPartyQueries
{
    private readonly bool _hasCreatedParty;

    public CrewPartyQueriesMock(bool hasCreatedParty)
    {
        _hasCreatedParty = hasCreatedParty;
    }
    
    public Task<bool> PlayerAlreadyInAParty(string captainId)
    {
        return Task.FromResult(_hasCreatedParty);
    }

    public Task<CrewParty?> GetCrewParty(string crewPartyId)
    {
        throw new NotImplementedException();
    }
}