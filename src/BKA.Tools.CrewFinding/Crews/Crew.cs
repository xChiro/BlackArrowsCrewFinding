using BKA.Tools.CrewFinding.Commons.Values;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.Crews;

public class Crew : Entity
{
    public Crew(string id, Player captain, CrewName name, Location reunionPoint, LanguageCollections languages,
        PlayerCollection players, Activity activity, DateTime createdAt)
    {
        Id = id;
        Players = players;
        Name = name;
        ReunionPoint = reunionPoint;
        Languages = languages;
        Activity = activity;
        CreatedAt = createdAt;
        Captain = captain;
        Players = players;
    }

    public Crew(Player captain, CrewName name, Location reunionPoint, LanguageCollections languages,
        PlayerCollection players, Activity activity) : this(Guid.NewGuid().ToString(), captain,
        name, reunionPoint, languages, players, activity, DateTime.UtcNow)
    {
    }

    public CrewName Name { get; }

    public Location ReunionPoint { get; }

    public LanguageCollections Languages { get; }

    public Activity Activity { get; }

    public PlayerCollection Players { get; }

    public DateTime CreatedAt { get; }

    public Player Captain { get; }

    public void AddMember(Player player)
    {
        Players.Add(player);
    }
}