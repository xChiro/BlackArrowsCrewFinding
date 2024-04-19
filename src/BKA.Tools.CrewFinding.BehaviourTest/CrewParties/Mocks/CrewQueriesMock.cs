using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public class CrewQueriesMock : ICrewQueriesMock
{
    private readonly Crew[] _crewParties;
    
    public IReadOnlyList<Crew> StoredCrewParties => _crewParties;

    public CrewQueriesMock(Crew[] crewParties)
    {
        _crewParties = crewParties;
    }

    public Task<Crew?> GetCrewParty(string crewPartyId)
    {
        return Task.FromResult(_crewParties.FirstOrDefault(p => p.Id == crewPartyId));
    }
}

public interface ICrewQueriesMock : ICrewQueries
{
    public IReadOnlyList<Crew> StoredCrewParties { get; }
}