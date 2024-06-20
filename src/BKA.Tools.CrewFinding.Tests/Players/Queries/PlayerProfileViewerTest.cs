using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Exceptions;
using BKA.Tools.CrewFinding.Players.Ports;
using BKA.Tools.CrewFinding.Players.Queries.PlayerProfiles;
using BKA.Tools.CrewFinding.Tests.Commons;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks.Crews;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks.Players;
using BKA.Tools.CrewFinding.Tests.Players.Mocks;

namespace BKA.Tools.CrewFinding.Tests.Players.Queries;

public class PlayerProfileViewerTest
{
    [Fact]
    public async void Attempt_To_View_A_Player_Profile_That_Does_Not_Exist_Should_Throw_Exception()
    {
        // Arrange
        var playerQueryRepositoryValidationMock = new PlayerQueryRepositoryValidationMock("453", "Rowan");
        var sut = new PlayerProfileViewer(playerQueryRepositoryValidationMock, new CrewQueryRepositoryEmptyMock());

        var playerProfileResponseMock = new PlayerProfileResponseMock();

        // Act & Assert
        await Assert.ThrowsAsync<PlayerNotFoundException>(() => sut.View("123", playerProfileResponseMock));
        playerProfileResponseMock.Player.Should().BeNull();
        ActiveCrewShouldBeEmpty(playerProfileResponseMock);
    }

    [Fact]
    public async void View_A_Player_Profile_Who_Is_Not_Part_Of_Any_Crew_Successfully()
    {
        // Arrange
        const string playerName = "Rowan";
        const string expectedPlayerId = "123";
        var playerQueryMock = CreatePlayerQueryRepository(expectedPlayerId, playerName);

        var sut = new PlayerProfileViewer(playerQueryMock, new CrewQueryRepositoryEmptyMock());
        var playerProfileResponseMock = new PlayerProfileResponseMock();

        // Act
        await sut.View(expectedPlayerId, playerProfileResponseMock);

        // Assert
        PlayerShouldBeValid(playerProfileResponseMock, expectedPlayerId, playerName);
        ActiveCrewShouldBeEmpty(playerProfileResponseMock);
    }

    [Fact]
    public async void View_A_Player_Profile_Who_Is_Part_Of_A_Crew_Successfully()
    {
        // Arrange
        const string playerName = "Rowan";
        const string expectedPlayerId = "123";
        const string expectedCrewId = "456";
        var expectedCrewName = new CrewName(playerName).Value;

        var playerQueryMock = CreatePlayerQueryRepository(expectedPlayerId, playerName);
        var crew = CrewBuilder.Build(expectedCrewId, Player.Create(expectedPlayerId, playerName, 2, 16));

        var sut = new PlayerProfileViewer(playerQueryMock,
            new CrewQueryRepositoryMock(crews: [crew], expectedPlayerId: expectedPlayerId));
        var playerProfileResponseMock = new PlayerProfileResponseMock();

        // Act
        await sut.View(expectedPlayerId, playerProfileResponseMock);

        // Assert
        PlayerShouldBeValid(playerProfileResponseMock, expectedPlayerId, playerName);
        playerProfileResponseMock.ActiveCrewId.Should().Be(expectedCrewId);
        playerProfileResponseMock.ActiveCrewName.Should().Be(expectedCrewName);
    }

    private static IPlayerQueryRepository CreatePlayerQueryRepository(string expectedPlayerId, string playerName)
    {
        IPlayerQueryRepository playerQueryMock = new PlayerQueryRepositoryValidationMock(expectedPlayerId, playerName);
        return playerQueryMock;
    }

    private static void PlayerShouldBeValid(PlayerProfileResponseMock playerProfileResponseMock, string expectedPlayerId,
        string playerName)
    {
        playerProfileResponseMock.Player.Should().NotBeNull();
        playerProfileResponseMock.Player!.Id.Should().Be(expectedPlayerId);
        playerProfileResponseMock.Player!.CitizenName.Value.Should().Be(playerName);
    }

    private static void ActiveCrewShouldBeEmpty(PlayerProfileResponseMock playerProfileResponseMock)
    {
        playerProfileResponseMock.ActiveCrewId.Should().BeEmpty();
        playerProfileResponseMock.ActiveCrewName.Should().BeEmpty();
    }
}