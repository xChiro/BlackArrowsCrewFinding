using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks;

public class CrewQueryRepositoryEmptyMock : ICrewQueryRepository
{
    public Task<Crew?> GetCrew(string crewId)
    {
        return Task.FromResult<Crew?>(null);
    }

    public Task<Crew[]> GetCrews(DateTime from, DateTime to)
    {
        return Task.FromResult(Array.Empty<Crew>());
    }
}