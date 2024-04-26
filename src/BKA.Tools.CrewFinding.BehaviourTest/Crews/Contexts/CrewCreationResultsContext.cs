using BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;

public class CrewCreationResultsContext
{
    public Exception Exception { get; set; } = null!;

    public CrewCreatorResponseMock CrewCreatorResponseMock { get; set; } = new();
}