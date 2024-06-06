using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks;

public class CrewQueriesRepositoryMock(IEnumerable<Crew>? crews = null, string expectedPlayerId = "") : ICrewQueryRepository
{
    public Task<Crew?> GetCrew(string crewId)
    {
        return Task.FromResult(crews?.FirstOrDefault(c => c.Id == crewId));
    }

    public Task<Crew[]> GetCrews(DateTime from, DateTime to)
    {
        var result = crews?.Where(c => c.CreatedAt >= from && c.CreatedAt <= to).ToArray();
        return Task.FromResult(result ?? []);
    }

    public Task<Crew[]> GetCrewsExpiredByDate(DateTime expiryDate)
    {
        var result = crews?.Where(c => c.CreatedAt <= expiryDate).ToArray();
        return Task.FromResult(result ?? []);
    }

    public Task<Crew?> GetActiveCrewByPlayerId(string playerId)
    {
        return expectedPlayerId == playerId ? Task.FromResult(crews?.FirstOrDefault()) : Task.FromResult<Crew?>(null);
    }
}