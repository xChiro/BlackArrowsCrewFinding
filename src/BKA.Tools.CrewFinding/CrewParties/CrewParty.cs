namespace BKA.Tools.CrewFinding.CrewParties;

public class CrewParty
{
    public CrewParty(CrewName name, Location reunionPoint, LanguageCollections languages, MaxCrewNumber maxCrewNumber,
        Activity activity, DateTime creationDate)
    {
        MaxCrewNumber = maxCrewNumber;
        Name = name;
        ReunionPoint = reunionPoint;
        Languages = languages;
        Activity = activity;
        CreationDate = creationDate;
    }

    public CrewName Name { get; }

    public Location ReunionPoint { get; }

    public LanguageCollections Languages { get; }

    public Activity Activity { get; }
    
    public MaxCrewNumber MaxCrewNumber { get;}
    
    public DateTime CreationDate { get; }
}