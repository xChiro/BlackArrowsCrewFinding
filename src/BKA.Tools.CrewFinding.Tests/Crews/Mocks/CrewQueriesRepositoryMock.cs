using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks;

public class CrewQueriesRepositoryMock(Crew? crew = null, IEnumerable<Crew>? crews = null) : ICrewQueryRepository
{
    public Task<Crew?> GetCrew(string crewId)
    {
        return Task.FromResult(crew?.Id == crewId ? crew : null);
    }

    public Task<Crew[]> GetCrews(DateTime from, DateTime to)
    {
        var crews1 = crews?.Where(c => c.CreatedAt >= from && c.CreatedAt <= to).ToArray();
        return Task.FromResult(crews1 ?? []);
    }
}