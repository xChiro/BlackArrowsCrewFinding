using BKA.Tools.CrewFinding.Azure.DataBase.Players;
using BKA.Tools.CrewFinding.Crews;

namespace BKA.Tools.CrewFinding.Azure.DataBase.CrewParties;

public class CrewDocument
{
    public string Id { get; set; }

    public string CaptainId { get; set; }

    public int MaxAllowed { get; set; }

    public string CrewName { get; set; }

    public string[] Language { get; set; }

    public string Activity { get; set; }

    public string Description { get; set; }

    public DateTime CreationAt { get; set; }

    public List<PlayerDocument> Members { get; set; }

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
            MaxAllowed = crew.Members.MaxAllowed,
            CrewName = crew.Name.Value,
            Language = crew.Languages.Select(l => l.LanguageCode).ToArray(),
            Activity = crew.Activity.Name,
            Description = crew.Activity.Description,
            CreationAt = crew.CreationAt,
            Members = crew.Members.Select(PlayerDocument.CreateFromPlayer).ToList(),
            System = crew.ReunionPoint.System,
            PlanetarySystem = crew.ReunionPoint.PlanetarySystem,
            PlanetMoon = crew.ReunionPoint.PlanetMoon,
            Place = crew.ReunionPoint.Place
        };

        return document;
    }
}