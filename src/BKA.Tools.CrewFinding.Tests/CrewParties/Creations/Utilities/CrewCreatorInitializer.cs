using BKA.Tools.CrewFinding.Crews.CreateRequests;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Creations.Utilities;

public static class CrewCreatorInitializer
{
    public static ICrewCreator InitializeCrewPartyCreator(ICrewCommandRepository crewCommandRepository,        
        bool hasCreatedParty = false, string captainName = "Captain", int maxPlayersAllowed = 4)
    {
        var sut = new CrewCreator(crewCommandRepository, new CrewQueryRepositoryMock(playerInCrew: hasCreatedParty),
            new PlayerQueryRepositoryAlwaysValidMock(captainName), maxPlayersAllowed);

        return sut;
    }

    public static ICrewCreator InitializeCrewPartyCreator(ICrewCommandRepository crewCommandRepository,
        PlayerQueryRepositoryValidationMock playerQueryRepositoryValidationMock)
    {
        var sut = new CrewCreator(crewCommandRepository, new CrewQueryRepositoryMock(),
            playerQueryRepositoryValidationMock, 4);

        return sut;
    }
}