using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.Crews;

public class CrewValidationRepositoryMock(bool playerInCrew = false) : ICrewValidationRepository
{
    public Task<bool> IsPlayerInActiveCrew(string playerId)
    {
        return Task.FromResult(playerInCrew);
    }
}