using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Players.Commands.Updates;
using BKA.Tools.CrewFinding.Players.Exceptions;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks.Players;
using PlayerCommandRepositoryMock = BKA.Tools.CrewFinding.Tests.Players.Mocks.PlayerCommandRepositoryMock;

namespace BKA.Tools.CrewFinding.Tests.Players.Commands;

public class HandleNameUpdaterTest
{
    private const int MinNameLength = 3;
    private const int MaxNameLength = 16;

    [Theory]
    [InlineData("")]
    [InlineData("th")]
    [InlineData("This is a very long name, so, should be invalid")]
    public async Task Attempt_To_Update_User_Profile_With_Invalid_Citizen_Name_Should_Throw_Exception(string newName)
    {
        // Arrange
        var playerCommandRepositoryMock = new PlayerCommandRepositoryMock();
        var sut = InitializeSut(playerCommandRepositoryMock, "1234", "1234");

        // Act & Assert
        await Assert.ThrowsAsync<HandlerNameLengthException>(() => sut.Update(newName));
        playerCommandRepositoryMock.NewName.Should().BeEmpty();
    }

    [Fact]
    public async Task Attempt_To_Update_User_Profile_That_Not_Exists_Should_Throw_Exception()
    {
        // Arrange
        const string userId = "124";
        var playerCommandRepositoryMock = new PlayerCommandRepositoryMock();
        var sut = InitializeSut(playerCommandRepositoryMock, userId);

        // Act & Assert
        await Assert.ThrowsAsync<PlayerNotFoundException>(() => sut.Update("New Citizen Name"));
        playerCommandRepositoryMock.NewName.Should().BeEmpty();
    }

    [Fact]
    public async Task Update_User_Profile_Successfully()
    {
        // Arrange
        const string userId = "124";
        const string newName = "Theren";
        var playerCommandRepositoryMock = new PlayerCommandRepositoryMock();
        var sut = InitializeSut(playerCommandRepositoryMock, userId, userId);

        // Act
        await sut.Update(newName);

        // Assert
        playerCommandRepositoryMock.NewName.Should().Be(newName);
    }

    private static HandleNameUpdater InitializeSut(PlayerCommandRepositoryMock playerCommandRepositoryMock,
        string sessionUserId, string storeUserId = "9999")
    {
        var userSession = new UserSessionMock(sessionUserId);
        var playerQueryRepositoryMock = new PlayerQueryRepositoryValidationMock(storeUserId, "Rowan");

        var sut = new HandleNameUpdater(playerQueryRepositoryMock, playerCommandRepositoryMock, userSession, MaxNameLength, MinNameLength);

        return sut;
    }
}