using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;

public class CrewQueriesRepositoryMock : ICrewQueryRepository
{
    private readonly Crew? _crew;
    
    public CrewQueriesRepositoryMock(Crew? crew = null)
    {
        _crew = crew;
    }
    
    public Task<Crew?> GetCrew(string crewId)
    {
        return Task.FromResult(_crew?.Id == crewId ? _crew : null);
    }

    public Task<Crew[]> GetCrews()
    {
        throw new System.NotImplementedException();
    }
}