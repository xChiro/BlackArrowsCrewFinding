using BKA.Tools.CrewFinding.CrewParties.Creators;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Tests.CrewPartyCreations.Mocks;

namespace BKA.Tools.CrewFinding.Tests.CrewPartyCreations.Utilities;

public static class CreatedCrewPartyUtilities
{
    public static CrewPartyCommandsMock InitializeCreateCrewPartyResultMock()
    {
        var createCrewPartyResultMock = new CrewPartyCommandsMock();

        return createCrewPartyResultMock;
    }

    public static ICrewPartyCreator InitializeCrewPartyCreator(ICrewPartyCommands crewPartyCommands,
        int maxCrewAllowed, bool hasCreatedParty = false)
    {
        var sut = new CrewPartyCreator(crewPartyCommands, new CrewPartyQueriesMock(hasCreatedParty), maxCrewAllowed);

        return sut;
    }

    public static ICrewPartyCreator InitializeCrewPartyCreator(ICrewPartyQueries crewPartyQueries, int maxCrewAllowed)
    {
        var sut = new CrewPartyCreator(new CrewPartyCommandsMock(), crewPartyQueries, maxCrewAllowed);

        return sut;
    }
}