using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.Crews;

public class CrewQueryRepositoryMock(IEnumerable<Crew>? crews = null)
    : ICrewQueryRepository
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
        return Task.FromResult(crews?.FirstOrDefault(c =>
            c.Captain.Id == playerId || c.Members.Any(m => m.Id == playerId)));
    }
}