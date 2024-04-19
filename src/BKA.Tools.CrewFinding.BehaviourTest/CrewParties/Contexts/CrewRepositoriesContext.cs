using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;

public class CrewRepositoriesContext
{
    public CrewCommandsMock CrewCommandsMock { get; set; } = new();
    public CrewQueriesMock CrewQueriesMocks { get; set; } = new(false);
}