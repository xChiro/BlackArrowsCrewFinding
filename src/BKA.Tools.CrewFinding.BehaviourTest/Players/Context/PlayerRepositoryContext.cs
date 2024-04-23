using BKA.Tools.CrewFinding.BehaviourTest.Players.Mocks;

namespace BKA.Tools.CrewFinding.BehaviourTest.Players.Context;

public class PlayerRepositoryContext
{
    public PlayerCommandRepositoryMock PlayerCommandRepositoryMock { get; set; } 
    
    public PlayerQueryRepositoryMock PlayerQueryRepositoryMock { get; set; }

    public PlayerRepositoryContext()
    {
        PlayerCommandRepositoryMock = new PlayerCommandRepositoryMock();
        PlayerQueryRepositoryMock = new PlayerQueryRepositoryMock("1", "Rowan");
    }
}