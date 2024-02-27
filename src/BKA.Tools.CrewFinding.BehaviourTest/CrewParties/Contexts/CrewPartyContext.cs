using BKA.Tools.CrewFinding.CrewParties;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;

public class CrewPartyContext
{
    public string Name { get; set; }
    public CrewOptions Options { get; set; }
    public PartyLocation Location { get; set; }
    public CrewActivity Activity { get; set; }

    public void FillData(string crewPartyName, Table crewPartyDetails)
    {
        Name = crewPartyName;
        Options = new CrewOptions
        {
            CrewSize = int.Parse(crewPartyDetails.Rows[0]["CrewSize"]),
            Languages = crewPartyDetails.Rows[0]["Languages"]
        };

        Location = new PartyLocation(crewPartyDetails.Rows[0]["System"], crewPartyDetails.Rows[0]["PlanetarySystem"],
            crewPartyDetails.Rows[0]["Planet/Moon"], crewPartyDetails.Rows[0]["Place"]);

        Activity = new CrewActivity
        {
            Description = crewPartyDetails.Rows[0]["Description"],
            Name = crewPartyDetails.Rows[0]["Activity"]
        };
    }

    public CrewPartyCreatorRequest ToRequest(string captainName)
    {
        var languages = Options.Languages.Split(',').ToList();
        languages.ForEach(l => l.Trim());

        var location = new Location(Location.System, Location.PlanetarySystem, Location.PlanetOrMoon, Location.Place);

        return new CrewPartyCreatorRequest(captainName, Options.CrewSize, location, languages.ToArray(), Activity.Name,
            Activity.Description);
    }
}