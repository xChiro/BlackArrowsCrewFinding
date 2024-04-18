using BKA.Tools.CrewFinding.CrewParties;

namespace BKA.Tools.CrewFinding.Azure.DataBase.Models;

public class CrewPartyDocument
{
    public string Id { get; set; }

    public string CaptainId { get; set; }

    public int Current { get; set; }

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

    public static CrewPartyDocument CreateFromCrewParty(CrewParty crewParty)
    {
        var document = new CrewPartyDocument
        {
            Id = crewParty.Id,
            CaptainId = crewParty.Captain.Id,
            Current = crewParty.CrewCapacity.Current,
            MaxAllowed = crewParty.CrewCapacity.MaxAllowed,
            CrewName = crewParty.Name.Value,
            Language = crewParty.Languages.Select(l => l.LanguageCode).ToArray(),
            Activity = crewParty.Activity.Name,
            Description = crewParty.Activity.Description,
            CreationAt = crewParty.CreationAt,
            Members = crewParty.Members.Select(PlayerDocument.CreateFromPlayer).ToList(),
            System = crewParty.ReunionPoint.System,
            PlanetarySystem = crewParty.ReunionPoint.PlanetarySystem,
            PlanetMoon = crewParty.ReunionPoint.PlanetMoon,
            Place = crewParty.ReunionPoint.Place
        };

        return document;
    }
}