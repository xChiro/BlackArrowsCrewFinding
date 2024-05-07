using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Commons.Exceptions;
using BKA.Tools.CrewFinding.Players.Commands.Creation;
using BKA.Tools.CrewFinding.Players.Exceptions;
using BKA.Tools.CrewFinding.Players.Ports;
using BKA.Tools.CrewFinding.Tests.Players.Mocks;

namespace BKA.Tools.CrewFinding.Tests.Players.Commands;

public class PlayerCreatorTests
{
    private const int MinNameLength = 3;
    private const int MaxNameLength = 30;

    [Theory]
    [InlineData("", "Rowan", typeof(UserIdInvalidException), "User Id is required")]
    [InlineData("24", "", typeof(CitizenNameEmptyException), "Star Citizen name is required")]
    public async Task Attempt_To_Create_User_Profile_With_Missing_Details_Should_Throw_Exception(
        string userId, string citizenName, Type exceptionType, string expectedExceptionMessage)
    {
        // Arrange
        var sut = GetSut();

        // Act & Assert
        var exception = await Assert.ThrowsAsync(exceptionType, () => sut.Create(userId, citizenName));

        // Assert
        exception.Message.Should().Be(expectedExceptionMessage);
    }

    [Theory]
    [InlineData("Ro", MinNameLength)]
    public async Task Attempt_To_Create_User_Profile_With_Invalid_Citizen_Name_Min_Length_Should_Throw_Exception(
        string citizenName, int minLength)
    {
        // Arrange
        var sut = GetSut(minNameLength: minLength);

        // Act & Assert
        await Assert.ThrowsAsync<CitizenNameLengthException>(() => sut.Create("123", citizenName));
    }

    [Theory]
    [InlineData("This is a very long name, so, should be invalid", 16)]
    public async Task Attempt_To_Create_User_Profile_With_Invalid_Citizen_Name_Max_Length_Should_Throw_Exception(
        string citizenName, int maxLength)
    {
        // Arrange
        var sut = GetSut(maxNameLength: maxLength);

        // Act & Assert
        await Assert.ThrowsAsync<CitizenNameLengthException>(() => sut.Create("123", citizenName));
    }

    [Fact]
    public async Task Create_User_Profile_Successfully()
    {
        // Arrange
        var playerCommandMock = new PlayerCommandRepositoryMock();
        var sut = GetSut(playerCommandMock);

        // Act
        await sut.Create("123", "Rowan");

        // Assert
        playerCommandMock.StoredPlayer.Should().NotBeNull();
        playerCommandMock.StoredPlayer!.Id.Should().Be("123");
        playerCommandMock.StoredPlayer!.CitizenName.Should().Be("Rowan");
    }

    private static IPlayerCreator GetSut(int minNameLength = MinNameLength, int maxNameLength = MaxNameLength)
    {
        return GetSut(new PlayerCommandRepositoryMock(), minNameLength, maxNameLength);
    }

    public static IPlayerCreator GetSut(IPlayerCommandRepository playerCommandRepository,
        int minNameLength = MinNameLength, int maxNameLength = MaxNameLength)
    {
        return new PlayerCreator(playerCommandRepository, minNameLength, maxNameLength);
    }
}