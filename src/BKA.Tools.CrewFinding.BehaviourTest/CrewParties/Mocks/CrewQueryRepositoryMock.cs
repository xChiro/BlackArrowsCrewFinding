using BKA.Tools.CrewFinding.Crews;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public class CrewQueryRepositoryMock : ICrewQueryRepositoryMock
{
    private readonly Crew[] _crews;
    private readonly bool _playerInCrew;

    public IReadOnlyList<Crew> StoredCrews => _crews;

    public CrewQueryRepositoryMock(Crew[] crews, bool playerInCrew = false)
    {
        _crews = crews;
        _playerInCrew = playerInCrew;
    }

    public Task<bool> IsPlayerInActiveCrew(string playerId)
    {
        return Task.FromResult(_playerInCrew);
    }

    public Task<Crew?> GetCrew(string crewId)
    {
        return Task.FromResult(_crews.FirstOrDefault(p => p.Id == crewId));
    }

    public Task<bool> IsActiveCrewOwnedBy(string crewId)
    {
        throw new NotImplementedException();
    }
}