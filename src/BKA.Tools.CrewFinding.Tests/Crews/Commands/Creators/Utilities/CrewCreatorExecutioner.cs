using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks;

namespace BKA.Tools.CrewFinding.Tests.Crews.Commands.Creators.Utilities;

public static class CrewCreatorExecutioner
{
    public static async Task Execute(ICrewCreator sut, ICrewCreatorResponse responseMock, string[]? languages = null,
        Location? expectedLocation = null,
        string activity = "Mining", int totalCrew = 2, string description = "Description")
    {
        var request = CrewPartyCreatorRequest(totalCrew,
            expectedLocation ?? Location.DefaultLocation(),
            languages ?? [], activity, description);

        await sut.Create(request, responseMock);
    }

    public static async Task Execute(ICrewCreator sut, int totalCrew = 2, string activity = "Mining",
        Location? expectedLocation = null,
        string description = "Description")
    {
        await Execute(sut, new CrewCreatorResponseMock(),
            languages: [], expectedLocation: expectedLocation,
            activity: activity, totalCrew: totalCrew, description: description);
    }

    private static CrewCreatorRequest CrewPartyCreatorRequest(int totalCrew, Location location, string[] languages,
        string activity, string description)
    {
        var request = new CrewCreatorRequest(totalCrew, location, languages, activity, description);

        return request;
    }
}