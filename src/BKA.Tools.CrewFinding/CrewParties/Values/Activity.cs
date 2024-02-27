namespace BKA.Tools.CrewFinding.CrewParties;

public record Activity(string Name, string Description)
{
    public static Activity Default()
    {
        return new Activity("Bounty Hunting", "Hunt down the most dangerous criminals in the galaxy.");
    }

    public static Activity Create(string activity, string description = "")
    {
        return activity is "" ? Default() : new Activity(activity, description);
    }
}