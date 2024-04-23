namespace BKA.Tools.CrewFinding.BehaviourTest.Players.Context;

public class PlayerContext
{
    public string PlayerName { get; set; }
    public string PlayerId { get; set; }
    public int MaxLength { get; set; } = 30;
    public int MinLength { get; set; } = 2;
}