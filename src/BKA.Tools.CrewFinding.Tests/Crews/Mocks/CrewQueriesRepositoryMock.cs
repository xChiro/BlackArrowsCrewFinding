using System;
using System.Linq;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks;

public class CrewQueriesRepositoryMock : ICrewQueryRepository
{
    private readonly Crew? _crew;
    private readonly Crew[]? _crews;

    public CrewQueriesRepositoryMock(Crew? crew = null, Crew[]? crews = null)
    {
        _crew = crew;
        _crews = crews;
    }
    
    public Task<Crew?> GetCrew(string crewId)
    {
        return Task.FromResult(_crew?.Id == crewId ? _crew : null);
    }

    public Task<Crew[]> GetCrews(DateTime from, DateTime to)
    {
        var crews = _crews?.Where(c => c.CreatedAt >= from && c.CreatedAt <= to).ToArray();
        return Task.FromResult(crews ?? []);
    }
}