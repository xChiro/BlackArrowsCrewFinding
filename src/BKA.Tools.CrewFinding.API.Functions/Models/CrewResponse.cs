using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.API.Functions.Models;

public class CrewResponse(
    string id,
    string name,
    string captainId,
    Activity activity,
    Location reunionPoint,
    LanguageCollections languages,
    int maxPlayers,
    int currentPlayers,
    string captainName,
    PlayerCollection members,
    DateTime createdAt)

{
    protected CrewResponse() : this("", "", "", Crews.Activity.Default(), Location.Default(),
        LanguageCollections.Default(), 0, 0, "", PlayerCollection.CreateEmpty(1), DateTime.MinValue)
    {
    }

    public string Id { get; protected set; } = id;
    public string Name { get; protected set; } = name;
    public string CaptainId { get; protected set; } = captainId;
    public string CaptainName { get; protected set; } = captainName;
    public PlayerCollection Members { get; protected set; } = members;
    public string Activity { get; protected set; } = activity.Name;
    public string Description { get; protected set; } = activity.Description;
    public Location ReunionPoint { get; protected set; } = reunionPoint;
    public string[] Languages { get; protected set; } = languages.Select(l => l.LanguageCode).ToArray();
    public int MaxPlayers { get; protected set; } = maxPlayers;
    public int CurrentPlayers { get; protected set; } = currentPlayers;
    public DateTime CreatedAt { get; protected set; } = createdAt;

    protected void UpdateFrom(Crew crew)
    {
        Id = crew.Id;
        Name = crew.Name.Value;
        CaptainId = crew.Captain.Id;
        Activity = crew.Activity.Name;
        Description = crew.Activity.Description;
        ReunionPoint = crew.ReunionPoint;
        Languages = crew.Languages.Select(l => l.LanguageCode).ToArray();
        MaxPlayers = crew.Members.MaxSize;
        CurrentPlayers = crew.Members.Count();
        CaptainName = crew.Captain.CitizenName.Value;
        Members = crew.Members;
        CreatedAt = crew.CreatedAt;
    }

    public static CrewResponse CreateFrom(Crew crew)
    {
        return new CrewResponse(
            crew.Id,
            crew.Name.Value,
            crew.Captain.Id,
            crew.Activity,
            crew.ReunionPoint,
            crew.Languages,
            crew.Members.MaxSize,
            crew.Members.Count(),
            crew.Captain.CitizenName.Value,
            crew.Members,
            crew.CreatedAt
        );
    }
}