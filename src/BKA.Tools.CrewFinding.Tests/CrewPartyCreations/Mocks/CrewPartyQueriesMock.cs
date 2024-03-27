using System.Threading.Tasks;
using BKA.Tools.CrewFinding.CrewParties.Ports;

namespace BKA.Tools.CrewFinding.Tests.CrewPartyCreations.Mocks;

public class CrewPartyQueriesMock(bool hasCreatedParty = false) : ICrewPartyQueries
{
    public string ReceivedCaptainName { get; private set; }

    public Task<bool> CaptainHasCreatedParty(string captainName)
    {
        ReceivedCaptainName = captainName;
        return Task.FromResult(hasCreatedParty);
    }
}