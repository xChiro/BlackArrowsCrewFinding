using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public class CrewQueryRepositoryMock : ICrewQueryRepositoryMock
{
    private readonly Crew[] _crewParties;
    
    public IReadOnlyList<Crew> StoredCrewParties => _crewParties;

    public CrewQueryRepositoryMock(Crew[] crewParties)
    {
        _crewParties = crewParties;
    }

    public Task<Crew?> GetCrew(string crewId)
    {
        return Task.FromResult(_crewParties.FirstOrDefault(p => p.Id == crewId));
    }
}

public interface ICrewQueryRepositoryMock : ICrewQueryRepository
{
    public IReadOnlyList<Crew> StoredCrewParties { get; }
}