using BKA.Tools.CrewFinding.Crews;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public class CrewNotFoundQueriesMock : ICrewQueriesMock
{
    public CrewNotFoundQueriesMock()
    {
        StoredCrewParties = new List<Crew>();
    }

    public IReadOnlyList<Crew> StoredCrewParties { get; }

    public Task<bool> PlayerAlreadyInACrew(string captainId)
    {
        return Task.FromResult(false);
    }

    public Task<Crew?> GetCrewParty(string crewPartyId)
    {
        return Task.FromResult<Crew?>(null);
    }
}