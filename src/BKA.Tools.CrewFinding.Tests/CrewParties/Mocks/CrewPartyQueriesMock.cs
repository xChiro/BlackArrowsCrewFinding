using System.Threading.Tasks;
using BKA.Tools.CrewFinding.CrewParties.Ports;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;

public class CrewPartyQueriesMock(bool playerInParty = false, string expectedCrewPartyId = "", CrewParty? crewParty = null)
    : ICrewPartyQueries
{
    public string ReceivedCaptainId { get; private set; } = string.Empty;

    public Task<bool> PlayerAlreadyInAParty(string captainId)
    {
        ReceivedCaptainId = captainId;
        return Task.FromResult(playerInParty);
    }

    public Task<CrewParty?> GetCrewParty(string crewPartyId)
    {
        return crewPartyId == expectedCrewPartyId ? Task.FromResult(crewParty) : Task.FromResult<CrewParty?>(null);
    }
}