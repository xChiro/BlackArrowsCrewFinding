using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;

public class CrewNotFoundValidationRepositoryMock : ICrewValidationRepository
{
    public Task<bool> IsPlayerInActiveCrew(string playerId)
    {
        return Task.FromResult(false);
    }

    public Task<Crew?> GetCrew(string crewId)
    {
        return Task.FromResult<Crew?>(null);
    }

    public Task<Crew[]> GetCrews(DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsActiveCrewOwnedBy(string crewId)
    {
        return Task.FromResult(false);
    }
}