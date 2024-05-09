using BKA.Tools.CrewFinding.Crews;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;

public class CrewQueryRepositoryMock(IReadOnlyList<Crew> crews, string expectedPlayerId = "") : ICrewQueryRepositryMock
{
    public IReadOnlyList<Crew> StoredCrews => crews;

    public Task<Crew?> GetCrew(string crewId)
    {
        return Task.FromResult(crews.FirstOrDefault(p => p.Id == crewId));
    }

    public Task<Crew[]> GetCrews(DateTime from, DateTime to)
    {
        return Task.FromResult(crews.Where(p => p.CreatedAt >= from && p.CreatedAt <= to).ToArray());
    }

    public Task<Crew?> GetActiveCrewByPlayerId(string playerId)
    {
        if (playerId == expectedPlayerId)
        {
            return Task.FromResult(crews.FirstOrDefault(p =>
                p.Captain.Id == playerId || p.Members.Any(m => m.Id == playerId)));
        }
        
        return Task.FromResult<Crew?>(null);
    }
}