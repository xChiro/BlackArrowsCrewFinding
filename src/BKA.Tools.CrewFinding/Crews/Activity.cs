using BKA.Tools.CrewFinding.Crews.Exceptions;

namespace BKA.Tools.CrewFinding.Crews;

public record Activity
{
    private Activity(string Name, string Description)
    {
        this.Name = Name;
        this.Description = Description;
    }

    public static Activity Default()
    {
        return new Activity("Bounty Hunting", "Hunt down the most dangerous criminals in the galaxy.");
    }

    public static Activity Create(string activity, string description = "")
    {
        if(activity.Length > 30)
            throw new ActivityNameLengthException(30);
        
        if(description.Length > 150)
            throw new ActivityDescriptionLengthException(150);
        
        return activity is "" ? Default() : new Activity(activity, description);
    }

    public string Name { get; }
    public string Description { get; }
}