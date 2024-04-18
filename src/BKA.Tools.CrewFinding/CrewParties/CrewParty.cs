using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.CrewParties;

public class CrewParty : Entity
{
    private readonly List<Player> _members;

    public CrewParty(string id, Player captain, CrewName name, Location reunionPoint, LanguageCollections languages,
        CrewCapacity crewCapacity, Activity activity, DateTime creationAt, List<Player> members)
    {
        Id = id;
        CrewCapacity = crewCapacity;
        Name = name;
        ReunionPoint = reunionPoint;
        Languages = languages;
        Activity = activity;
        CreationAt = creationAt;
        Captain = captain;
        _members = members;
    }

    public CrewParty(Player captain, CrewName name, Location reunionPoint, LanguageCollections languages,
        CrewCapacity crewCapacity, Activity activity) : this(Guid.NewGuid().ToString(), captain,
        name, reunionPoint, languages, crewCapacity, activity, new DateTime(), new List<Player>())
    {
    }

    public CrewName Name { get; }

    public Location ReunionPoint { get; }

    public LanguageCollections Languages { get; }

    public Activity Activity { get; }

    public CrewCapacity CrewCapacity { get; }

    public DateTime CreationAt { get; }

    public Player Captain { get; }

    public IEnumerable<Player> Members => _members.AsReadOnly();

    public bool IsFull()
    {
        return CrewCapacity.IsAtCapacity();
    }

    public void AddMember(Player player)
    {
        _members.Add(player);
    }
}