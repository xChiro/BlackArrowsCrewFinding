using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Commons.Values;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks;

namespace BKA.Tools.CrewFinding.Tests.Crews.Commands.Creators.Utilities;

public static class CrewCreationExecutioner
{
    public static async Task ExecuteCrewCreation(ICrewCreator sut, ICrewCreatorResponse responseMock,
        string captainId = "1ASD34-344SDF", string[]? languages = null, Location? expectedLocation = null,
        string activity = "Mining", int totalCrew = 2, string description = "Description")
    {
        var request = CrewPartyCreatorRequest(totalCrew,
            expectedLocation ?? Location.DefaultLocation(),
            languages ?? Array.Empty<string>(), activity, description);

        await sut.Create(request, responseMock);
    }

    public static async Task ExecuteCrewCreation(ICrewCreator sut, string captainId = "1ASD34-344SDF",
        int totalCrew = 2, string activity = "Mining", Location? expectedLocation = null,
        string description = "Description")
    {
        await ExecuteCrewCreation(sut, new CrewCreatorResponseMock(), captainId: captainId,
            languages: Array.Empty<string>(), expectedLocation: expectedLocation,
            activity: activity, totalCrew: totalCrew, description: description);
    }

    private static CrewCreatorRequest CrewPartyCreatorRequest(int totalCrew, Location location, string[] languages,
        string activity, string description)
    {
        var request = new CrewCreatorRequest(totalCrew, location, languages, activity, description);

        return request;
    }
}