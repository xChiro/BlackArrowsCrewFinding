using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public class CrewValidationRepositoryMock : ICrewValidationRepositoryMock, ICrewQueryRepository
{
    private readonly Crew[] _crews;
    private readonly bool _playerInCrew;
    private readonly bool _playerIsOwner;

    public IReadOnlyList<Crew> StoredCrews => _crews;

    public CrewValidationRepositoryMock(Crew[] crews, bool playerInCrew = false, bool playerIsOwner = false)
    {
        _crews = crews;
        _playerInCrew = playerInCrew;
        _playerIsOwner = playerIsOwner;
    }

    public Task<bool> IsPlayerInActiveCrew(string playerId)
    {
        return Task.FromResult(_playerInCrew);
    }

    public Task<Crew?> GetCrew(string crewId)
    {
        return Task.FromResult(_crews.FirstOrDefault(p => p.Id == crewId));
    }

    public Task<Crew[]> GetCrews(DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsActiveCrewOwnedBy(string crewId)
    {
        return Task.FromResult(_playerIsOwner);
    }
}