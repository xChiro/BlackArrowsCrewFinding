using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;

public class CrewQueryRepositoryMock : ICrewQueryRepository
{
    private readonly Crew? _crew;
    private readonly bool _playerInCrew;
    private readonly bool _playerOwnedCrew;

    public CrewQueryRepositoryMock(Crew? crew = null, bool playerInCrew = false, bool playerOwnedCrew = false)
    {
        _crew = crew;
        _playerInCrew = playerInCrew;
        _playerOwnedCrew = playerOwnedCrew;
    }

    public Task<bool> IsPlayerInActiveCrew(string playerId)
    {
        return Task.FromResult(_playerInCrew);
    }

    public Task<Crew?> GetCrew(string crewId)
    {
        return Task.FromResult(_crew?.Id == crewId ? _crew : null);
    }

    public Task<bool> IsActiveCrewOwnedBy(string crewId)
    {
        return Task.FromResult(_playerOwnedCrew);
    }
}