namespace BKA.Tools.CrewFinding.Values;
public record CrewNumber
{
    public int Value { get; }
    
    public CrewNumber(int input, int maxAllowed)
    {
        Value = input < 0 || input > maxAllowed ? maxAllowed : input;
    }
}