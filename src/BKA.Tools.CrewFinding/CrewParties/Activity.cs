namespace BKA.Tools.CrewFinding.CrewParties;

public record Activity
{
    public string Value { get; }

    public Activity(string value)
    {
        Value = value;
    }

    public static Activity Default()
    {
        return new Activity("Bounty Hunting");
    }

    public static Activity Create(string activity)
    {
        return activity is "" ? Default() : new Activity(activity);
    }
}