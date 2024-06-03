using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Commands.Leave;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Tests.Commons.Mocks;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks;

namespace BKA.Tools.CrewFinding.Tests.Crews.Commands.Leave;

public class CrewLeaverTest
{
    private const string PlayerId = "1";
    private const string PlayerName = "Allan";
    private const string CrewId = "1234";

    [Fact]
    public void LeavingNonexistentCrewShouldThrowException()
    {
        // Arrange
        var sut = InitializeCrewLeaver(PlayerId);

        // Act
        var func = () => sut.Leave();

        // Assert
        func.Should().ThrowAsync<CrewNotFoundException>();
    }

    [Fact]
    public void LeavingCrewWithoutBeingMemberShouldThrowException()
    {
        // Arrange
        var sut = InitializeCrewLeaver(PlayerId);

        // Act
        var func = () => sut.Leave();

        // Assert
        func.Should().ThrowAsync<PlayerNotInCrewException>();
    }

    [Fact]
    public async Task LeavingCrewShouldBeSuccessful()
    {
        // Arrange
        var playerToLeave = CreatePlayer(PlayerId, PlayerName);
        var crew = InitializeCrew(playerToLeave, CrewId);
        var crewCommandMock = CreateCrewCommandRepositoryMock();
        var sut = InitializeCrewLeaver(crewCommandMock, crew);

        // Act
        await sut.Leave();

        // Assert
        crewCommandMock.Members.Should().NotContain(player => player.Id == playerToLeave.Id);
        crew.Members.Should().NotContain(player => player.Id == playerToLeave.Id);
    }

    private static Player CreatePlayer(string playerId, string playerName)
    {
        return Player.Create(playerId, playerName);
    }

    private static CrewCommandRepositoryMock CreateCrewCommandRepositoryMock()
    {
        return new CrewCommandRepositoryMock();
    }

    private static CrewLeaver InitializeCrewLeaver(string playerId)
    {
        var player = CreatePlayer(playerId, PlayerName);
        return InitializeCrewLeaver(CreateCrewCommandRepositoryMock(), InitializeCrew(player, CrewId));
    }

    private static CrewLeaver InitializeCrewLeaver(ICrewCommandRepository crewCommandMock, Crew crew)
    {
        var crewQueryRepository = new CrewQueriesRepositoryMock(crews: [crew], expectedPlayerId: PlayerId);
        return new CrewLeaver(crewQueryRepository, crewCommandMock, new UserSessionMock(PlayerId));
    }

    private static Crew InitializeCrew(Player playerToLeave, string crewId)
    {
        return new Crew(crewId,
            playerToLeave,
            Location.Default(),
            LanguageCollections.Default(),
            PlayerCollection.CreateWithSingle(playerToLeave, 1), 
            Activity.Default(),
            DateTime.UtcNow);
    }
}