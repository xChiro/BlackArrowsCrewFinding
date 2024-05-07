namespace BKA.Tools.CrewFinding.Crews;

public record CrewName
{
    public string Value { get; }

    public CrewName(string captainName)
    {
        Value = $"Crew of {captainName}";
    }
}