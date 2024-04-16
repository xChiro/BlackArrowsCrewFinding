using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.CrewParties.CreateRequests;
using BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.Utilities;

public static class CrewCreationExecutioner
{
    public static async Task ExecuteCrewCreation(ICrewPartyCreator sut, ICrewPartyCreatorResponse responseMock,
        string captainId = "1ASD34-344SDF", string captainName = "Rowan", string[]? languages = null,
        Location? expectedLocation = null,
        string activity = "Mining", int totalCrew = 2, string description = "Description")
    {
        var request = CrewPartyCreatorRequest(captainId, captainName, totalCrew, expectedLocation ?? Location.DefaultLocation(), languages ?? Array.Empty<string>(), activity, description);

        await sut.Create(request, responseMock);
    }

    public static async Task ExecuteCrewCreation(ICrewPartyCreator sut, string captainId = "1ASD34-344SDF",
        int totalCrew = 2, string activity = "Mining", Location? expectedLocation = null,
        string description = "Description", string captainName = "Rowan")
    {
        await ExecuteCrewCreation(sut, new CrewPartyCreatorResponseMock(), captainId: captainId, captainName: captainName, languages: Array.Empty<string>(), expectedLocation: expectedLocation, activity: activity, totalCrew: totalCrew, description: description);
    }

    private static CrewPartyCreatorRequest CrewPartyCreatorRequest(string captainId, string captainName, int totalCrew,
        Location location, string[] languages, string activity, string description)
    {
        var request = new CrewPartyCreatorRequest(captainId, captainName, totalCrew, location, languages, activity,
            description);

        return request;
    }
}