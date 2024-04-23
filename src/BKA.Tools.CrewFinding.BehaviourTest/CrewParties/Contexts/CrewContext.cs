using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts.Values;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.CreateRequests;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;

public class CrewContext
{
    public CrewOptions Options { get; set; } = new();
    public CrewLocation CrewLocation { get; set; } = new();
    public CrewActivity Activity { get; set; } = new();
    public int MaxPlayerAllowed { get; set; } = 4;

    public void FillData(Table crewPartyDetails)
    {
        Options = new CrewOptions
        {
            CrewSize = int.Parse(crewPartyDetails.Rows[0]["CrewSize"]),
            Languages = crewPartyDetails.Rows[0]["Languages"]
        };

        CrewLocation = new CrewLocation(crewPartyDetails.Rows[0]["System"], crewPartyDetails.Rows[0]["PlanetarySystem"],
            crewPartyDetails.Rows[0]["Planet/Moon"], crewPartyDetails.Rows[0]["Place"]);

        Activity = new CrewActivity
        {
            Description = crewPartyDetails.Rows[0]["Description"],
            Name = crewPartyDetails.Rows[0]["Activity"]
        };
    }

    public CrewCreatorRequest ToRequest(string captainName)
    {
        var languages = Options.Languages.Split(',').Select(x => x.Trim()).ToArray();
        var location = new Location(CrewLocation.System, CrewLocation.PlanetarySystem, CrewLocation.PlanetOrMoon, CrewLocation.Place);

        return new CrewCreatorRequest(captainName, Options.CrewSize, location, languages.ToArray(), Activity.Name,
            Activity.Description);
    }

    public Crew ToCrew(string captainId, string captainName)
    {
        var captain = Player.Create(captainId, captainName);
        
        return new Crew(captain, 
            new CrewName("captainName"),
            CrewLocation.ToLocation(),
            LanguageCollections.Default(),
            Members.CreateEmpty(MaxPlayerAllowed),
            CrewFinding.Values.Activity.Default());
    }
}