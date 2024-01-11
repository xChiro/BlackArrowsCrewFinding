namespace BKA.Tools.CrewFinding.CrewParties;

public record CrewName
{
    public string Value { get; }

    public CrewName(string captainName)
    {
        Value = captainName.EndsWith("s") ? $"{captainName}' CrewParty" : $"{captainName}'s CrewParty";
    }
}