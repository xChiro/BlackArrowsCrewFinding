using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;

public class CrewPartyRepositoriesContext
{
    public CrewPartyCommandsMock CrewPartyCommandsMock { get; set; } = new();
    public CrewPartyQueriesMock CrewPartyQueriesMocks { get; set; } = new(false);
}