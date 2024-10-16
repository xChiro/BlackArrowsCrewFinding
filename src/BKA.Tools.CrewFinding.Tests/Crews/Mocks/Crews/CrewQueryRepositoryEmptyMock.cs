using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.Crews;

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

    public Task<Crew[]> GetCrewsExpiredByDate(DateTime expiryDate)
    {
        return Task.FromResult(Array.Empty<Crew>());
    }

    public Task<Crew?> GetActiveCrewByPlayerId(string playerId)
    {
        return Task.FromResult<Crew?>(null);
    }
}