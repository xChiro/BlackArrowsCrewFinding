namespace BKA.Tools.CrewFinding.Tests.CreateCrewParties;

public static class CreatedCrewPartyUtilities
{
    public static CrewPartyCommandsMock InitializeCreateCrewPartyResultMock()
    {
        var createCrewPartyResultMock = new CrewPartyCommandsMock();

        return createCrewPartyResultMock;
    }

    public static ICrewPartyCreator InitializeCrewPartyCreator(ICrewPartyCommands crewPartyCommandsMock,
        int maxCrewAllowed)
    {
        var sut = new CrewPartyCreator(crewPartyCommandsMock, maxCrewAllowed);

        return sut;
    }
}