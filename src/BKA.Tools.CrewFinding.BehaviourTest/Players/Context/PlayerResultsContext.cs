using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.BehaviourTest.Players.Context;

public class PlayerResultsContext
{
    public Exception? Exception { get; set; } = null;
    public Player? Player { get; set; } = null;
}