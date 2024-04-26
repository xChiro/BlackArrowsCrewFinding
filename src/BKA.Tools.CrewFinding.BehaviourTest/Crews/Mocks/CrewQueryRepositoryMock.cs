using BKA.Tools.CrewFinding.Crews;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;

public class CrewQueryRepositoryMock : ICrewQueryRepositryMock
{
    private readonly Crew[] _crews;

    public IReadOnlyList<Crew> StoredCrews => _crews;

    public CrewQueryRepositoryMock(Crew[] crews)
    {
        _crews = crews;
    }

    public Task<Crew?> GetCrew(string crewId)
    {
        return Task.FromResult(_crews.FirstOrDefault(p => p.Id == crewId));
    }

    public Task<Crew[]> GetCrews(DateTime from, DateTime to)
    {
        return Task.FromResult(_crews.Where(p => p.CreatedAt >= from && p.CreatedAt <= to).ToArray());
    }
}