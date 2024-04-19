using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;

public class CrewPartyCreationResultsContext
{
    public Exception Exception { get; set; } = null!;

    public CrewPartyCreatorResponseMock CrewPartyCreatorResponseMock { get; set; } = new();
}