using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Crews;

public class Crew : Entity
{
    public Crew(string id, Player captain, CrewName name, Location reunionPoint, LanguageCollections languages,
        Members members, Activity activity, DateTime creationAt)
    {
        Id = id;
        Members = members;
        Name = name;
        ReunionPoint = reunionPoint;
        Languages = languages;
        Activity = activity;
        CreationAt = creationAt;
        Captain = captain;
        Members = members;
    }

    public Crew(Player captain, CrewName name, Location reunionPoint, LanguageCollections languages,
        Members members, Activity activity) : this(Guid.NewGuid().ToString(), captain,
        name, reunionPoint, languages, members, activity, DateTime.UtcNow)
    {
    }

    public CrewName Name { get; }

    public Location ReunionPoint { get; }

    public LanguageCollections Languages { get; }

    public Activity Activity { get; }

    public Members Members { get; }

    public DateTime CreationAt { get; }

    public Player Captain { get; }

    public void AddMember(Player player)
    {
        Members.AddMember(player);
    }
}