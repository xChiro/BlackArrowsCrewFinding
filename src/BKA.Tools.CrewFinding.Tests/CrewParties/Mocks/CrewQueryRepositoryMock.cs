using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;

public class CrewQueryRepositoryMock : ICrewQueryRepository
{
    private readonly string _expectedCrewPartyId;
    private readonly Crew? _crewParty;
    private readonly bool _playerInCrew;

    public CrewQueryRepositoryMock(string expectedCrewPartyId = "", Crew? crewParty = null, bool playerInCrew = false)
    {
        _expectedCrewPartyId = expectedCrewPartyId;
        _crewParty = crewParty;
        _playerInCrew = playerInCrew;
    }

    public Task<bool> IsPlayerInActiveCrew(string playerId)
    {
        return Task.FromResult(_playerInCrew);
    }

    public Task<Crew?> GetCrew(string crewId)
    {
        return crewId == _expectedCrewPartyId ? Task.FromResult(_crewParty) : Task.FromResult<Crew?>(null);
    }
}