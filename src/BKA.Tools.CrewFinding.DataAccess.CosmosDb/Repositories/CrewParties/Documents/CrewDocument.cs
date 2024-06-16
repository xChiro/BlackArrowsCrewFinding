using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.Players.Documents;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties.Documents;

public class CrewDocument
{
    public string Id { get; set; }

    public string CaptainId { get; set; }

    public string CaptainName { get; set; }
    
    public int MaxAllowed { get; set; }

    public string CrewName { get; set; }

    public string[] Language { get; set; }

    public string ActivityName { get; set; }

    public string ActivityDescription { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<PlayerDocument> Crew { get; set; }

    public string System { get; set; }

    public string PlanetarySystem { get; set; }

    public string PlanetMoon { get; set; }

    public string Place { get; set; }

    public static CrewDocument CreateFromCrew(Crew crew)
    {
        var document = new CrewDocument
        {
            Id = crew.Id,
            CaptainId = crew.Captain.Id,
            CaptainName = crew.Captain.CitizenName.Value,
            MaxAllowed = crew.Members.MaxSize,
            CrewName = crew.Name.Value,
            Language = crew.Languages.Select(l => l.LanguageCode).ToArray(),
            ActivityName = crew.Activity.Name,
            ActivityDescription = crew.Activity.Description,
            CreatedAt = crew.CreatedAt,
            Crew = crew.Members.Select(PlayerDocument.CreateFromPlayer).ToList(),
            System = crew.ReunionPoint.System,
            PlanetarySystem = crew.ReunionPoint.PlanetarySystem,
            PlanetMoon = crew.ReunionPoint.PlanetMoon,
            Place = crew.ReunionPoint.Place
        };

        return document;
    }

    public Crew ToCrew(int minNameLength, int maxNameLength)
    {
        var members = Crew.Select(m => Player.Create(m.Id, m.CitizenName, minNameLength, maxNameLength)).ToList();

        var crew = new Crew(
            Id,
             Player.Create(CaptainId, CaptainName, minNameLength, maxNameLength),
            new Location(System, PlanetarySystem, PlanetMoon, Place),
            LanguageCollections.CreateFromAbbrevs(Language),
            PlayerCollection.Create(members, MaxAllowed),
            Activity.Create(ActivityName, ActivityDescription),
            CreatedAt);

        return crew;
    }
}