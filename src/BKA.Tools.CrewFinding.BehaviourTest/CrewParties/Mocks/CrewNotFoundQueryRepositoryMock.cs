using BKA.Tools.CrewFinding.Crews;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public class CrewNotFoundQueryRepositoryMock : ICrewQueryRepositoryMock
{
    public CrewNotFoundQueryRepositoryMock()
    {
        StoredCrews = new List<Crew>();
    }

    public IReadOnlyList<Crew> StoredCrews { get; }

    public Task<bool> IsPlayerInActiveCrew(string playerId)
    {
        return Task.FromResult(false);
    }

    public Task<Crew?> GetCrew(string crewId)
    {
        return Task.FromResult<Crew?>(null);
    }
}