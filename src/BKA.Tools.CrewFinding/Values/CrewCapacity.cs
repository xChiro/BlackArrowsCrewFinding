namespace BKA.Tools.CrewFinding.Values;

public record CrewCapacity
{
    public int Current { get; }
    
    public int MaxAllowed { get; }
    
    public CrewCapacity(int current, int maxAllowed)
    {
        MaxAllowed = maxAllowed;
        Current = current <= 0 || current > maxAllowed ? maxAllowed : current;
    }

    public bool IsAtCapacity()
    {
        return Current == MaxAllowed;
    }
}