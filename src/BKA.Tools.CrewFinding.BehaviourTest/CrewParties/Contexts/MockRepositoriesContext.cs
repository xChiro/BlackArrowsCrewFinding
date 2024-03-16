using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;
using BKA.Tools.CrewFinding.CrewParties.Ports;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;

public class MockRepositoriesContext
{
    public ICrewPartyCommandsMock CrewPartyCommandsMock { get; set; } = new CrewPartyCommandsMock();
    public ICrewPartyQueries CrewPartyQueriesMocks { get; set; } = new CrewPartyQueriesMock(false);
}