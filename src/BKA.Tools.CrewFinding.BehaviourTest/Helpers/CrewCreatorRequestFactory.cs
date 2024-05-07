using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;

namespace BKA.Tools.CrewFinding.BehaviourTest.Helpers;

public static class CrewCreatorRequestFactory
{
    private const int DefaultTotalCrew = 4;
    private const string DefaultActivity = "Bounty Hunting";
    private const string DefaultDescription = "Hunt down the most dangerous criminals in the galaxy.";
    private static readonly string[] DefaultLanguages = ["en"];
    private static readonly Location DefaultLocation = new("Sol", "Sol", "Earth", "Paris");
    private static readonly Location EmptyLocation = new("", "", "", "");

    public static CrewCreatorRequest CreateCrew(int totalCrew = DefaultTotalCrew)
    {
        return CreateCrewPartyRequest(totalCrew, DefaultLocation, DefaultLanguages, DefaultActivity,
            DefaultDescription);
    }

    public static CrewCreatorRequest CreateDefaultCrewPartyWithoutLocation()
    {
        return CreateCrewPartyRequest(DefaultTotalCrew, EmptyLocation, DefaultLanguages, DefaultActivity,
            DefaultDescription);
    }

    public static CrewCreatorRequest CreateCrewPartyWithMissingLanguages()
    {
        return CreateCrewPartyRequest(DefaultTotalCrew, DefaultLocation, [],
            DefaultActivity, DefaultDescription);
    }

    private static CrewCreatorRequest CreateCrewPartyRequest(int totalCrew, Location location,
        string[] languages, string activity, string description)
    {
        return new CrewCreatorRequest(totalCrew, location, languages, activity, description);
    }
}