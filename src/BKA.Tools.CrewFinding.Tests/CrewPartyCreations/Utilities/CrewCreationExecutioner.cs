using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.CrewParties.Creators;
using BKA.Tools.CrewFinding.Tests.CrewPartyCreations.Mocks;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.CrewPartyCreations.Utilities;

public static class CrewCreationExecutioner
{
    public static async Task ExecuteCrewCreation(ICrewPartyCreator sut, ICrewPartyCreatorResponse responseMock,
        string captainId = "1ASD34-344SDF", string[]? languages = null,
        Location? expectedLocation = null, string activity = "Mining", int totalCrew = 2,
        string description = "Description")
    {
        var request = CrewPartyCreatorRequest(captainId, totalCrew,
            expectedLocation ?? Location.DefaultLocation(),
            languages ?? Array.Empty<string>(), activity, description);

        await sut.Create(request, responseMock);
    }

    public static async Task ExecuteCrewCreation(ICrewPartyCreator sut, string captainId = "1ASD34-344SDF",
        int totalCrew = 2, string activity = "Mining", Location? expectedLocation = null,
        string description = "Description")
    {
        await ExecuteCrewCreation(sut, new CrewPartyCreatorResponseMock(), captainId: captainId,
            languages: Array.Empty<string>(), expectedLocation: expectedLocation,
            activity: activity, totalCrew: totalCrew, description: description);
    }

    private static CrewPartyCreatorRequest CrewPartyCreatorRequest(string captainId, int totalCrew,
        Location location,
        string[] languages, string activity, string description)
    {
        var request = new CrewPartyCreatorRequest(captainId, totalCrew, location, languages, activity,
            description);

        return request;
    }
}