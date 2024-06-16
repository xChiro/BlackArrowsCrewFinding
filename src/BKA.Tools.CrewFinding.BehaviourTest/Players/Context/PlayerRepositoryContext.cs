using BKA.Tools.CrewFinding.BehaviourTest.Players.Mocks;

namespace BKA.Tools.CrewFinding.BehaviourTest.Players.Context;

public class PlayerRepositoryContext
{
    public PlayerCommandRepositoryMock PlayerCommandRepositoryMock { get; set; } = new(2, 16);

    public PlayerQueryRepositoryMock PlayerQueryRepositoryMock { get; set; } = new("1", "Rowan");
}