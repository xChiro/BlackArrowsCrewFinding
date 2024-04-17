using System.Threading.Tasks;
using BKA.Tools.CrewFinding.CrewParties.Ports;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;

public class CrewPartyQueriesMock : ICrewPartyQueries
{
    private readonly bool _playerInParty;
    private readonly string _expectedCrewPartyId;
    private readonly CrewParty? _crewParty;

    public CrewPartyQueriesMock(bool playerInParty = false, string expectedCrewPartyId = "", CrewParty? crewParty = null)
    {
        _playerInParty = playerInParty;
        _expectedCrewPartyId = expectedCrewPartyId;
        _crewParty = crewParty;
    }

    public string ReceivedCaptainId { get; private set; } = string.Empty;

    public Task<bool> PlayerAlreadyInAParty(string captainId)
    {
        ReceivedCaptainId = captainId;
        return Task.FromResult(_playerInParty);
    }

    public Task<CrewParty?> GetCrewParty(string crewPartyId)
    {
        return crewPartyId == _expectedCrewPartyId ? Task.FromResult(_crewParty) : Task.FromResult<CrewParty?>(null);
    }
}