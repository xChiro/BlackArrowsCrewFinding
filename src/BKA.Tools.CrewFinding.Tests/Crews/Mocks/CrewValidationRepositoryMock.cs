using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks;

public class CrewValidationRepositoryMock : ICrewValidationRepository 
{
    private readonly bool _playerInCrew;
    private readonly bool _playerOwnedCrew;

    public CrewValidationRepositoryMock(bool playerInCrew = false, bool playerOwnedCrew = false)
    {
        _playerInCrew = playerInCrew;
        _playerOwnedCrew = playerOwnedCrew;
    }

    public Task<bool> IsPlayerInActiveCrew(string playerId)
    {
        return Task.FromResult(_playerInCrew);
    }

    public Task<bool> IsActiveCrewOwnedBy(string crewId)
    {
        return Task.FromResult(_playerOwnedCrew);
    }
}