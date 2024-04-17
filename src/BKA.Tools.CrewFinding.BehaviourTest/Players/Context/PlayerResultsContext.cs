using BKA.Tools.CrewFinding.BehaviourTest.Players.Mocks;

namespace BKA.Tools.CrewFinding.BehaviourTest.Players.Context;

public class PlayerResultsContext
{
    public PlayerCommandRepositoryMock PlayerCommandRepositoryMock { get; set; }
    public Exception Exception { get; set; }

    public PlayerResultsContext()
    {
        PlayerCommandRepositoryMock = new PlayerCommandRepositoryMock();
    }
}