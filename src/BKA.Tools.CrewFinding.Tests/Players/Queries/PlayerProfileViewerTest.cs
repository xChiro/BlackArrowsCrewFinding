using BKA.Tools.CrewFinding.Players.Exceptions;
using BKA.Tools.CrewFinding.Players.Ports;
using BKA.Tools.CrewFinding.Players.Queries.PlayerProfiles;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks;

namespace BKA.Tools.CrewFinding.Tests.Players.Queries;

public class PlayerProfileViewerTest
{
    [Fact]
    public async void Attempt_To_View_A_Player_Profile_That_Does_Not_Exist_Should_Throw_Exception()
    {
        // Arrange
        var sut = new PlayerProfileViewer(new PlayerQueryRepositoryValidationMock("453", "Rowan"));

        // Act & Assert
        await Assert.ThrowsAsync<PlayerNotFoundException>(() => sut.View("123"));
    }

    [Fact]
    public async void View_A_Player_Profile_Successfully()
    {
        // Arrange
        const string playerName = "Rowan";
        const string expectedPlayerId = "123";

        IPlayerQueryRepository playerQueryMock = new PlayerQueryRepositoryValidationMock(expectedPlayerId, playerName);
        var sut = new PlayerProfileViewer(playerQueryMock);

        // Act
        var player = await sut.View(expectedPlayerId);

        // Assert
        player.Id.Should().Be(expectedPlayerId);
        player.CitizenName.Should().Be(playerName);
    }
}