using BKA.Tools.CrewFinding.CrewParties;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.BehaviourTest.Globals;

public class CrewPartyFactory
{
    public static CrewPartyCreatorRequest CreateDefaultCrewParty(string captainName)
    {
        return new CrewPartyCreatorRequest(captainName, 4,
            new Location("Sol", "Sol", "Earth", "Paris"), new[] {"en"}, "Bounty Hunting",
            "Hunt down the most dangerous criminals in the galaxy.");
    }

    public static CrewPartyCreatorRequest CreateDefaultCrewPartyWithOutLocation(string captainName = "Rowan")
    {
        const string planetarySystem = "";

        return new CrewPartyCreatorRequest(captainName, 4,
            new Location(planetarySystem, planetarySystem, planetarySystem, planetarySystem), new[] {"en"},
            "Bounty Hunting",
            "Hunt down the most dangerous criminals in the galaxy.");
    }
}