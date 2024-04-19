using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;

public class CrewCreationResultsContext
{
    public Exception Exception { get; set; } = null!;

    public CrewCreatorResponseMock CrewCreatorResponseMock { get; set; } = new();
}