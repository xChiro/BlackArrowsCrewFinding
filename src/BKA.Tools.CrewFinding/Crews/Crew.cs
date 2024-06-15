using BKA.Tools.CrewFinding.Commons;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.Crews;

public class Crew : Entity
{
    public Crew(string id, Player captain, Location reunionPoint, LanguageCollections languages,
        PlayerCollection members, Activity activity, DateTime createdAt)
    {
        Id = id;
        Members = members;
        Name = new CrewName(captain.CitizenName.Value);
        ReunionPoint = reunionPoint;
        Languages = languages;
        Activity = activity;
        CreatedAt = createdAt;
        Captain = captain;
        Members = members;
    }

    public Crew(Player captain, Location reunionPoint, LanguageCollections languages,
        PlayerCollection members, Activity activity) : this(Guid.NewGuid().ToString(), captain, reunionPoint, languages,
        members, activity, DateTime.UtcNow)
    {
    }

    public CrewName Name { get; }

    public Location ReunionPoint { get; }

    public LanguageCollections Languages { get; }

    public Activity Activity { get; }

    public PlayerCollection Members { get; }

    public DateTime CreatedAt { get; }

    public Player Captain { get; }

    public void AddMember(Player player)
    {
        Members.Add(player);
    }

    public bool RemoveMember(string playerId)
    {
        return Members.Remove(playerId);
    }
}