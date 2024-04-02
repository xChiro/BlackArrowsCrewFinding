using System.Threading.Tasks;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Ports;

namespace BKA.Tools.CrewFinding.Tests.CrewPartyCreations.Mocks;

public class CrewPartyQueriesMock(bool hasCreatedParty = false) : ICrewPartyQueries
{
    public string ReceivedCaptainId { get; private set; } = string.Empty;

    public Task<bool> PlayerHasCreatedParty(string captainId)
    {
        ReceivedCaptainId = captainId;
        return Task.FromResult(hasCreatedParty);
    }
}