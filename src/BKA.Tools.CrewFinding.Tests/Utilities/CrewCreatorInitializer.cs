using BKA.Tools.CrewFinding.Crews.CreateRequests;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;

namespace BKA.Tools.CrewFinding.Tests.Utilities;

public static class CrewCreatorInitializer
{
    public static ICrewCreator InitializeCrewPartyCreator(ICrewCommandRepository crewCommandRepository,
        int maxCrewAllowed = 4, bool hasCreatedParty = false, string captainName = "Captain")
    {
        var sut = new CrewCreator(crewCommandRepository, new CrewQueryRepositoryMock(playerInCrew: hasCreatedParty), maxCrewAllowed,
            new PlayerQueryRepositoryAlwaysValidMock(captainName));

        return sut;
    }

    public static ICrewCreator InitializeCrewPartyCreator(ICrewCommandRepository crewCommandRepository,
        PlayerQueryRepositoryValidationMock playerQueryRepositoryValidationMock)
    {
        var sut = new CrewCreator(crewCommandRepository, new CrewQueryRepositoryMock(), 4, playerQueryRepositoryValidationMock);

        return sut;
    }
}