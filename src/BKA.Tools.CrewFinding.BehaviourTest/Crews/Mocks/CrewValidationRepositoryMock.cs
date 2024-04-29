using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;

public class CrewValidationRepositoryMock : ICrewValidationRepository
{
    private readonly bool _playerInCrew;
    private readonly bool _playerIsOwner;

    public CrewValidationRepositoryMock(bool playerInCrew = false, bool playerIsOwner = false)
    {
        _playerInCrew = playerInCrew;
        _playerIsOwner = playerIsOwner;
    }

    public Task<bool> IsPlayerInActiveCrew(string playerId)
    {
        return Task.FromResult(_playerInCrew);
    }

    public Task<bool> IsActiveCrewOwnedBy(string crewId)
    {
        return Task.FromResult(_playerIsOwner);
    }
}