using BKA.Tools.CrewFinding.CrewParties.CreateRequests;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;

namespace BKA.Tools.CrewFinding.Tests.Utilities;

public static class CrewPartyCreatorInitializer
{
    public static ICrewPartyCreator InitializeCrewPartyCreator(ICrewPartyCommands crewPartyCommands,
        int maxCrewAllowed = 4, bool hasCreatedParty = false, string captainName = "Captain")
    {
        var sut = new CrewPartyCreator(crewPartyCommands, new CrewPartyQueriesMock(hasCreatedParty), maxCrewAllowed,
            new PlayerQueriesAlwaysValidMock(captainName));

        return sut;
    }

    public static ICrewPartyCreator InitializeCrewPartyCreator(ICrewPartyQueries crewPartyQueries,
        int maxCrewAllowed = 4, string captainName = "Captain")
    {
        var sut = new CrewPartyCreator(new CrewPartyCommandsMock(), crewPartyQueries, maxCrewAllowed,
            new PlayerQueriesAlwaysValidMock(captainName));

        return sut;
    }

    public static ICrewPartyCreator InitializeCrewPartyCreator(ICrewPartyCommands crewPartyCommands,
        PlayerQueriesValidationMock playerQueriesValidationMock)
    {
        var sut = new CrewPartyCreator(crewPartyCommands, new CrewPartyQueriesMock(), 4, playerQueriesValidationMock);

        return sut;
    }
}