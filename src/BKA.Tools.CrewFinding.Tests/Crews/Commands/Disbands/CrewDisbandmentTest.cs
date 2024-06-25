using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Commands.Disbands;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Tests.Commons;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks.Crews;

namespace BKA.Tools.CrewFinding.Tests.Crews.Commands.Disbands;

public class CrewDisbandmentTest
{
    private const string CrewId = "12314212";

    [Fact]
    public async Task Attempt_To_Disband_Crew_That_Is_Not_Owned_By_Player_Should_Throw_Exception_Async()
    {
        // Arrange
        var crewDisbandmentResponseMock = new CrewDisbandmentResponseMock();
        var sut = SetupSutNotOwner(CreatePlayer(Guid.NewGuid().ToString(), "Adam"));

        // Act
        var act = async () => await sut.Disband(crewDisbandmentResponseMock);

        // Assert
        await act.Should().ThrowAsync<CrewDisbandException>();
        crewDisbandmentResponseMock.CrewId.Should().BeEmpty();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("123412")]
    public async Task Disband_Crew_That_Exists_And_Is_Owned_By_Player_Should_Be_Successful_Async(string? voiceChannelId)
    {
        // Arrange
        var crewCommandRepositoryMock = new CrewCommandRepositoryMock();
        var crewDisbandmentResponseMock = new CrewDisbandmentResponseMock();
        var sut = SetupSutOwner(CreatePlayer(Guid.NewGuid().ToString(), "Adam"), crewCommandRepositoryMock);

        // Act
        await sut.Disband(crewDisbandmentResponseMock);

        // Assert
        crewCommandRepositoryMock.DisbandedCrewIds.Should().Contain(CrewId);
        crewDisbandmentResponseMock.CrewId.Should().Be(CrewId);
    }

    private static Player CreatePlayer(string playerId, string playerName = "playerName")
    {
        const int playerMinLength = 2;
        const int playerMaxLength = 16;

        return Player.Create(playerId, playerName, playerMinLength, playerMaxLength);
    }

    private static CrewDisbandment SetupSutOwner(Player player, CrewCommandRepositoryMock crewCommandRepositoryMock)
    {
        var userSession = CreateUserSessionMock(player);
        var crew = CrewBuilder.Build(CrewId, player);
        var crewValidationRepository = new CrewQueryRepositoryMock(crews: [crew]);
        var sut = CreateCrewDisbandment(crewValidationRepository, crewCommandRepositoryMock, userSession);

        return sut;
    }

    private static CrewDisbandment SetupSutNotOwner(Player player)
    {
        var userSession = CreateUserSessionMock(player);
        var crewValidationRepository = new CrewQueryRepositoryMock();
        var crewCommandRepositoryMock = new CrewCommandRepositoryMock();
        var sut = CreateCrewDisbandment(crewValidationRepository, crewCommandRepositoryMock, userSession);

        return sut;
    }

    private static CrewDisbandment CreateCrewDisbandment(CrewQueryRepositoryMock crewValidationRepository,
        CrewCommandRepositoryMock crewDisbandRepository, UserSessionMock userSession)
    {
        var sut = new CrewDisbandment(crewValidationRepository, crewDisbandRepository, userSession);
        return sut;
    }

    private static UserSessionMock CreateUserSessionMock(Player player)
    {
        var userSession = new UserSessionMock(player.Id);
        return userSession;
    }
}

public class CrewDisbandmentResponseMock : ICrewDisbandmentResponse
{
    public string CrewId { get; set; } = string.Empty;


    public void SetResult(string crewId)
    {
        CrewId = crewId;
    }
}