using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.API.Functions.Queries.RecentCrews;

public class RecentCrewResponse(
    string id,
    string name,
    Player captain,
    Activity activity,
    Location reunionPoint,
    LanguageCollections languages,
    DateTime createdAt)
{
    public string Id { get; set; } = id;
    public string Name { get; set; } = name;
    public Player Captain { get; set; } = captain;
    public Activity Activity { get; set; } = activity;
    public Location ReunionPoint { get; set; } = reunionPoint;
    public LanguageCollections Languages { get; set; } = languages;
    public DateTime CreatedAt { get; set; } = createdAt;

    public static RecentCrewResponse CreateFrom(Crew crew)
    {
        return new RecentCrewResponse(
            crew.Id,
            crew.Name.ToString(),
            crew.Captain,
            crew.Activity,
            crew.ReunionPoint,
            crew.Languages,
            crew.CreatedAt
        );
    }
}