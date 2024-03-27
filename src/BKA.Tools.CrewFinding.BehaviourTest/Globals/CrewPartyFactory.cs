using BKA.Tools.CrewFinding.CrewParties.Creators;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.BehaviourTest.Globals;

public class CrewPartyFactory
{
    private const string DefaultCaptainName = "Rowan";
    private const int DefaultTotalCrew = 4;
    private const string DefaultActivity = "Bounty Hunting";
    private const string DefaultDescription = "Hunt down the most dangerous criminals in the galaxy.";
    private static readonly string[] DefaultLanguages = {"en"};
    private static readonly Location DefaultLocation = new("Sol", "Sol", "Earth", "Paris");
    private static readonly Location EmptyLocation = new("", "", "", "");

    public static CrewPartyCreatorRequest CreateCrewParty(string captainName = DefaultCaptainName,
        int totalCrew = DefaultTotalCrew)
    {
        return CreateCrewPartyRequest(captainName, totalCrew, DefaultLocation, DefaultLanguages, DefaultActivity,
            DefaultDescription);
    }

    public static CrewPartyCreatorRequest CreateDefaultCrewPartyWithoutLocation(string captainName = DefaultCaptainName)
    {
        return CreateCrewPartyRequest(captainName, DefaultTotalCrew, EmptyLocation, DefaultLanguages, DefaultActivity,
            DefaultDescription);
    }

    public static CrewPartyCreatorRequest CreateCrewPartyWithMissingLanguages(string captainName = DefaultCaptainName)
    {
        return CreateCrewPartyRequest(captainName, DefaultTotalCrew, DefaultLocation, Array.Empty<string>(),
            DefaultActivity, DefaultDescription);
    }

    private static CrewPartyCreatorRequest CreateCrewPartyRequest(string captainName, int totalCrew, Location location,
        string[] languages, string activity, string description)
    {
        return new CrewPartyCreatorRequest(captainName, totalCrew, location, languages, activity, description);
    }
}