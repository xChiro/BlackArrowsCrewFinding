using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.CrewParties.Creators;
using BKA.Tools.CrewFinding.Tests.CrewPartyCreations.Mocks;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.CrewPartyCreations.Utilities;

public static class ExecuteCrewCreationUtilities
{
    public static async Task ExecuteCrewCreation(ICrewPartyCreator sut, ICrewPartyCreatorResponse responseMock,
        string[]? languages = null, Location? expectedLocation = null, string activity = "Mining",
        string captainName = "Rowan", int totalCrew = 2, string description = "Description")
    {
        var request = CrewPartyCreatorRequest(captainName, totalCrew, expectedLocation ?? Location.DefaultLocation(),
            languages ?? Array.Empty<string>(), activity, description);

       await sut.Create(request, responseMock);
    }

    public static async Task ExecuteCrewCreation(ICrewPartyCreator sut, string captainName = "Rowan", int totalCrew = 2,
        string activity = "Mining", Location? expectedLocation = null, string description = "Description")
    {
        await ExecuteCrewCreation(sut, new CrewPartyCreatorResponseMock(), Array.Empty<string>(), expectedLocation,
            activity, captainName, totalCrew, description);
    }

    private static CrewPartyCreatorRequest CrewPartyCreatorRequest(string captainName, int totalCrew, Location location,
        string[] languages, string activity, string description)
    {
        var request = new CrewPartyCreatorRequest(captainName, totalCrew, location, languages, activity, description);

        return request;
    }
}