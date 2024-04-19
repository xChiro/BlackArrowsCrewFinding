using BKA.Tools.CrewFinding.Crews.CreateRequests;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;

namespace BKA.Tools.CrewFinding.Tests.Utilities;

public static class CrewCreatorInitializer
{
    public static ICrewCreator InitializeCrewPartyCreator(ICrewCommands crewCommands,
        int maxCrewAllowed = 4, bool hasCreatedParty = false, string captainName = "Captain")
    {
        var sut = new CrewCreator(crewCommands, new CrewQueriesMock(), maxCrewAllowed,
            new PlayerQueriesAlwaysValidMock(captainName, hasCreatedParty));

        return sut;
    }

    public static ICrewCreator InitializeCrewPartyCreator(ICrewCommands crewCommands,
        PlayerQueriesValidationMock playerQueriesValidationMock)
    {
        var sut = new CrewCreator(crewCommands, new CrewQueriesMock(), 4, playerQueriesValidationMock);

        return sut;
    }
}