using BKA.Tools.CrewFinding.BehaviourTest.Players.Mocks;

namespace BKA.Tools.CrewFinding.BehaviourTest.Players.Context;

public class PlayerRepositoryContext
{
    public PlayerCommandRepositoryMock PlayerCommandRepositoryMock { get; set; } 
    
    public PlayerQueriesMock PlayerQueriesMock { get; set; }

    public PlayerRepositoryContext()
    {
        PlayerCommandRepositoryMock = new PlayerCommandRepositoryMock();
        PlayerQueriesMock = new PlayerQueriesMock("1", "Rowan");
    }
}