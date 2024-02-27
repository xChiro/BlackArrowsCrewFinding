namespace BKA.Tools.CrewFinding.CrewParties;
public record MaxCrewNumber
{
    public int Value { get; }
    
    public MaxCrewNumber(int input, int maxAllowed)
    {
        Value = (input < 0 || input > maxAllowed) ? maxAllowed : input;
    }
}