using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts.Values;
using BKA.Tools.CrewFinding.Commons.Values;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Commands.CreateRequests;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;

public class CrewContext
{
    public CrewOptions Options { get; set; } = new();
    public CrewLocation CrewLocation { get; set; } = new();
    public CrewActivity CrewActivity { get; set; } = new();
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

        CrewActivity = new CrewActivity
        {
            Description = crewPartyDetails.Rows[0]["Description"],
            Name = crewPartyDetails.Rows[0]["Activity"]
        };
    }

    public CrewCreatorRequest ToRequest(string captainName)
    {
        var languages = Options.Languages.Split(',').Select(x => x.Trim()).ToArray();
        var location = new Location(CrewLocation.System, CrewLocation.PlanetarySystem, CrewLocation.PlanetOrMoon, CrewLocation.Place);

        return new CrewCreatorRequest(captainName, Options.CrewSize, location, languages.ToArray(), CrewActivity.Name,
            CrewActivity.Description);
    }

    public Crew ToCrew(string captainId, string captainName)
    {
        var captain = Player.Create(captainId, captainName);
        
        return new Crew(captain, 
            new CrewName("captainName"),
            CrewLocation.ToLocation(),
            LanguageCollections.Default(),
            PlayerCollection.CreateEmpty(MaxPlayerAllowed),
            Activity.Default());
    }
}