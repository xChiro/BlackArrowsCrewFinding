using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Cultures;

namespace BKA.Tools.CrewFinding.API.Functions.Queries.RecentCrews;

public class RecentCrewResponse(
    string id,
    string name,
    string captainId,
    Activity activity,
    Location reunionPoint,
    LanguageCollections languages,
    int maxPlayers,
    int currentPlayers)
{
    public string Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string CaptainId { get; set; } = captainId;
    public string Activity { get; set; } = activity.Name;
    public string Description { get; set; } = activity.Description;
    public Location ReunionPoint { get; set; } = reunionPoint;
    public string[] Languages { get; set; } = languages.Select(l => l.LanguageCode).ToArray();
    public int MaxPlayers { get; set; } = maxPlayers;
    public int CurrentPlayers { get; set; } = currentPlayers;

    public static RecentCrewResponse CreateFrom(Crew crew)
    {
        return new RecentCrewResponse(
            crew.Id,
            crew.Name.Value,
            crew.Captain.Id,
            crew.Activity,
            crew.ReunionPoint,
            crew.Languages,
            crew.Members.MaxSize,
            crew.Members.Count()
        );
    }
}