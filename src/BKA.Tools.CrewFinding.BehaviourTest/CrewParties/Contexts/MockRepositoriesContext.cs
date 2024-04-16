using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;

public class MockRepositoriesContext
{
    public CrewPartyCommandsMock CrewPartyCommandsMock { get; } = new();
    public CrewPartyQueriesMock CrewPartyQueriesMocks { get; set; } = new(false);
}