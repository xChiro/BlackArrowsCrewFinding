using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;

public class CrewQueryRepositoryMock : ICrewQueryRepository
{
    private readonly string _expectedCrewPartyId;
    private readonly Crew? _crewParty;

    public CrewQueryRepositoryMock(string expectedCrewPartyId = "", Crew? crewParty = null)
    {
        _expectedCrewPartyId = expectedCrewPartyId;
        _crewParty = crewParty;
    }

    public string ReceivedCaptainId { get; private set; } = string.Empty;

    public Task<Crew?> GetCrew(string crewId)
    {
        ReceivedCaptainId = crewId;
        return crewId == _expectedCrewPartyId ? Task.FromResult(_crewParty) : Task.FromResult<Crew?>(null);
    }
}