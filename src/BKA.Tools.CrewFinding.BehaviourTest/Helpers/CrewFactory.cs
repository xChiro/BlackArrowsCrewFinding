using BKA.Tools.CrewFinding.Crews.CreateRequests;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.BehaviourTest.Helpers;

public class CrewFactory
{
    private const int DefaultTotalCrew = 4;
    private const string DefaultActivity = "Bounty Hunting";
    private const string DefaultDescription = "Hunt down the most dangerous criminals in the galaxy.";
    private static readonly string[] DefaultLanguages = {"en"};
    private static readonly Location DefaultLocation = new("Sol", "Sol", "Earth", "Paris");
    private static readonly Location EmptyLocation = new("", "", "", "");

    public static CrewCreatorRequest CreateCrew(string captainId,
        int totalCrew = DefaultTotalCrew)
    {
        return CreateCrewPartyRequest(captainId, totalCrew, DefaultLocation, DefaultLanguages, DefaultActivity,
            DefaultDescription);
    }

    public static CrewCreatorRequest CreateDefaultCrewPartyWithoutLocation(string captainId)
    {
        return CreateCrewPartyRequest(captainId, DefaultTotalCrew, EmptyLocation, DefaultLanguages, DefaultActivity,
            DefaultDescription);
    }

    public static CrewCreatorRequest CreateCrewPartyWithMissingLanguages(string captainId)
    {
        return CreateCrewPartyRequest(captainId, DefaultTotalCrew, DefaultLocation, Array.Empty<string>(),
            DefaultActivity, DefaultDescription);
    }

    private static CrewCreatorRequest CreateCrewPartyRequest(string captainId, int totalCrew, Location location,
        string[] languages, string activity, string description)
    {
        return new CrewCreatorRequest(captainId, totalCrew, location, languages, activity, description);
    }
}