using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;

public class CreatePartyResultsContext
{
    public ICrewPartyCommandsMock CrewPartyCommandsMock { get; set; } = new CrewPartyCommandsMock();
}