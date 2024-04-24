namespace BKA.Tools.CrewFinding.Commons.Values;

public record CrewName
{
    public string Value { get; }

    public CrewName(string captainName)
    {
        Value = $"Crew of {captainName}";
    }
}