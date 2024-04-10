namespace BKA.Tools.CrewFinding.Values;

public record CrewName
{
    public string Value { get; }

    public CrewName(string captainName)
    {
        Value = $"Crew Party of {captainName}";
    }
}