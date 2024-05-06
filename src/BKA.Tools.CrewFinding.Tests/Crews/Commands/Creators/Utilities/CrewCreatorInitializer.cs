using BKA.Tools.CrewFinding.Crews.Commands.Creators;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Tests.Commons.Mocks;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks;

namespace BKA.Tools.CrewFinding.Tests.Crews.Commands.Creators.Utilities;

public static class CrewCreatorInitializer
{
    public static ICrewCreator InitializeCrewPartyCreator(ICrewCommandRepository crewCommandRepository,
        bool hasCreatedParty = false, string captainName = "Captain", int maxPlayersAllowed = 4,
        string playerId = "1ASD34-344SDF")
    {
        var sut = new CrewCreator(crewCommandRepository,
            new CrewValidationRepositoryMock(playerInCrew: hasCreatedParty),
            new PlayerQueryRepositoryAlwaysValidMock(captainName), new UserSessionMock(playerId), maxPlayersAllowed);

        return sut;
    }

    public static ICrewCreator InitializeCrewPartyCreator(ICrewCommandRepository crewCommandRepository,
        PlayerQueryRepositoryValidationMock playerQueryRepositoryValidationMock, string playerId = "1ASD34-344SDF")

    {
        var sut = new CrewCreator(crewCommandRepository, new CrewValidationRepositoryMock(),
            playerQueryRepositoryValidationMock, new UserSessionMock(playerId), 4);

        return sut;
    }
}