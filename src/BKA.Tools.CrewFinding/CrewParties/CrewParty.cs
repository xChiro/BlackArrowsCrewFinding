using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.CrewParties;

public class CrewParty
{
    public CrewParty(string id, CrewName name, Location reunionPoint, LanguageCollections languages,
        CrewCapacity totalCrewCapacity,
        Activity activity, DateTime creationDate, Player captain)
    {
        TotalCrewCapacity = totalCrewCapacity;
        Name = name;
        ReunionPoint = reunionPoint;
        Languages = languages;
        Activity = activity;
        CreationDate = creationDate;
        Captain = captain;
        Id = id;
    }

    public CrewParty(CrewName name, Location reunionPoint, LanguageCollections languages,
        CrewCapacity totalCrewCapacity,
        Activity activity, DateTime creationDate, Player captain) : this(Guid.NewGuid().ToString(), name, reunionPoint,
        languages, totalCrewCapacity, activity, creationDate, captain)
    {
    }

    public string Id { get; }

    public CrewName Name { get; }

    public Location ReunionPoint { get; }

    public LanguageCollections Languages { get; }

    public Activity Activity { get; }

    public CrewCapacity TotalCrewCapacity { get; }

    public DateTime CreationDate { get; }

    public Player Captain { get; }

    public bool IsFull()
    {
        return TotalCrewCapacity.IsFull();
    }
}