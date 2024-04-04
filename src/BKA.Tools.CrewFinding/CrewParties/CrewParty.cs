using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.CrewParties;

public class CrewParty
{
    public CrewParty(CrewName name, Location reunionPoint, LanguageCollections languages, CrewCapacity totalCrewCapacity,
        Activity activity, DateTime creationDate)
    {
        TotalCrewCapacity = totalCrewCapacity;
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
    
    public CrewCapacity TotalCrewCapacity { get;}
    
    public DateTime CreationDate { get; }

    public bool IsFull()
    {
        return TotalCrewCapacity.IsFull();
    }
}