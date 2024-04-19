using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;

public class CrewQueriesMock : ICrewQueries
{
    private readonly bool _playerInParty;
    private readonly string _expectedCrewPartyId;
    private readonly Crew? _crewParty;

    public CrewQueriesMock(bool playerInParty = false, string expectedCrewPartyId = "", Crew? crewParty = null)
    {
        _playerInParty = playerInParty;
        _expectedCrewPartyId = expectedCrewPartyId;
        _crewParty = crewParty;
    }

    public string ReceivedCaptainId { get; private set; } = string.Empty;

    public Task<bool> PlayerAlreadyInACrew(string captainId)
    {
        ReceivedCaptainId = captainId;
        return Task.FromResult(_playerInParty);
    }

    public Task<Crew?> GetCrewParty(string crewPartyId)
    {
        return crewPartyId == _expectedCrewPartyId ? Task.FromResult(_crewParty) : Task.FromResult<Crew?>(null);
    }
}