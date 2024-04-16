using BKA.Tools.CrewFinding.CrewParties.CreateRequests;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.BehaviourTest.Globals;

public class CrewPartyFactory
{
    private const int DefaultTotalCrew = 4;
    private const string DefaultCaptain = "Rowan";
    private const string DefaultActivity = "Bounty Hunting";
    private const string DefaultDescription = "Hunt down the most dangerous criminals in the galaxy.";
    private static readonly string[] DefaultLanguages = {"en"};
    private static readonly Location DefaultLocation = new("Sol", "Sol", "Earth", "Paris");
    private static readonly Location EmptyLocation = new("", "", "", "");

    public static CrewPartyCreatorRequest CreateCrewParty(string captainId, string captainName = "Rowan",
        int totalCrew = DefaultTotalCrew)
    {
        return CreateCrewPartyRequest(captainId, captainName, totalCrew, DefaultLocation, DefaultLanguages, DefaultActivity,
            DefaultDescription);
    }

    public static CrewPartyCreatorRequest CreateDefaultCrewPartyWithoutLocation(string captainId)
    {
        return CreateCrewPartyRequest(captainId, DefaultCaptain, DefaultTotalCrew, EmptyLocation, DefaultLanguages, DefaultActivity,
            DefaultDescription);
    }

    public static CrewPartyCreatorRequest CreateCrewPartyWithMissingLanguages(string captainId)
    {
        return CreateCrewPartyRequest(captainId, DefaultCaptain, DefaultTotalCrew, DefaultLocation, Array.Empty<string>(),
            DefaultActivity, DefaultDescription);
    }

    private static CrewPartyCreatorRequest CreateCrewPartyRequest(string captainId, string captainName, int totalCrew,
        Location location,
        string[] languages, string activity, string description)
    {
        return new CrewPartyCreatorRequest(captainId, captainName, totalCrew, location, languages, activity,
            description);
    }
}