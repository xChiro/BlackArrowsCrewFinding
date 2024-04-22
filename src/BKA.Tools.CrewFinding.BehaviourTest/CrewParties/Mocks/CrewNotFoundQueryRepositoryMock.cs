using BKA.Tools.CrewFinding.Crews;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public class CrewNotFoundQueryRepositoryMock : ICrewQueryRepositoryMock
{
    public CrewNotFoundQueryRepositoryMock()
    {
        StoredCrewParties = new List<Crew>();
    }

    public IReadOnlyList<Crew> StoredCrewParties { get; }

    public Task<bool> PlayerAlreadyInACrew(string captainId)
    {
        return Task.FromResult(false);
    }

    public Task<Crew?> GetCrew(string crewId)
    {
        return Task.FromResult<Crew?>(null);
    }
}