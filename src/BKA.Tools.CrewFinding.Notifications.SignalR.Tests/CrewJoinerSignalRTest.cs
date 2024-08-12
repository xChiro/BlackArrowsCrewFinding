using BKA.Tools.CrewFinding.Crews.Commands.JoinRequests;
using BKA.Tools.CrewFinding.Notifications.SignalR.Crews;
using BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks;
using BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.Crews;
using BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.SignalR;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests;

public class CrewJoinerSignalRTest
{
    [Fact]
    public async void Attempt_To_Sent_JoinMessage_To_Crew_But_Joiner_Fails_Throw_Exception()
    {
        // Arrange
        var crewJoinerFailMock = new CrewJoinerFailMock<ArgumentException>();
        var signalRGroupServiceMock = new SignalRGroupServiceMock();
        var sut = CreateCrewJoinerSignalR(crewJoinerFailMock, signalRGroupServiceMock);

        // Act
        var act = async () => await sut.Join("crewId");

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async void Attempt_To_Sent_JoinMessage_To_Crew_But_SignalR_Fails_Return_Success()
    {
        // Arrange
        const string crewId = "crewId";
        var crewJoinerMock = new CrewJoinerMock();
        var signalRGroupServiceMock = new SignalRGroupServiceExceptionMock<ArgumentException>();
        var domainLoggerMock = new DomainLoggerMock();
        var sut = CreateCrewJoinerSignalR(crewJoinerMock, signalRGroupServiceMock, domainLoggerMock);

        // Act
        await sut.Join(crewId);

        // Assert
        crewJoinerMock.CrewId.Should().Be(crewId);
        domainLoggerMock.LastException.Should().BeOfType<ArgumentException>();
    }

    [Fact]
    public async void Send_Join_Player_To_SignalR_Group_Successfully()
    {
        // Arrange
        const string crewId = "crewId";
        const string connectionId = "1231412";
        var crewJoinerMock = new CrewJoinerMock();
        var signalRGroupServiceMock = new SignalRGroupServiceMock();
        var sut = CreateCrewJoinerSignalR(crewJoinerMock, signalRGroupServiceMock, null, connectionId);

        // Act
        await sut.Join(crewId);

        // Assert
        signalRGroupServiceMock.UserId.Should().Be(connectionId);
        signalRGroupServiceMock.GroupName.Should().Be(crewId);
        signalRGroupServiceMock.Message.Should().NotBeNull();
    }

    [Fact]
    public async void Attempt_To_Send_Join_Player_To_SignalR_Group_But_Get_Player_Name_Fails_Continue_Successfully()
    {
        // Arrange
        const string crewId = "crewId";
        var domainLoggerMock = new DomainLoggerMock();
        var crewJoinerMock = new CrewJoinerMock();
        var playerQueryRepositoryMock = new PlayerQueryRepositoryExceptionMock<ArgumentException>();
        var sut = CreateCrewJoinerSignalR(crewJoinerMock, domainLoggerMock: domainLoggerMock,
            playerQueryRepository: playerQueryRepositoryMock);

        // Act
        await sut.Join(crewId);

        // Assert
        domainLoggerMock.LastException.Should().BeOfType<ArgumentException>();
        crewJoinerMock.CrewId.Should().Be(crewId);
    }

    [Fact]
    public async void Send_PlayerName_To_SignalR_Group_Successfully()
    {
        // Arrange
        const string userId = "1238012x";
        const string crewId = "crewId";
        const string playerName = "Rowan";
        var crewJoinerMock = new CrewJoinerMock();
        var playerQueryRepositoryMock = new PlayerQueryRepositoryMock(playerName);
        var signalRGroupServiceMock = new SignalRGroupServiceMock();
        var sut = CreateCrewJoinerSignalR(crewJoinerMock, playerQueryRepository: playerQueryRepositoryMock,
            signalRGroupService: signalRGroupServiceMock, userId: userId);

        // Act
        await sut.Join(crewId);

        // Assert
        signalRGroupServiceMock.Message.Should().NotBeNull();
        signalRGroupServiceMock.Message!.Should().BeEquivalentTo(new
        {
            PlayerId = userId,
            CitizenName = playerName
        });
        signalRGroupServiceMock.ExcludedUserIds.Should().Contain(userId);
    }

    private static CrewJoinerSignalR CreateCrewJoinerSignalR(ICrewJoiner crewJoinerMock,
        ISignalRGroupService? signalRGroupService = null, DomainLoggerMock? domainLoggerMock = null,
        string userId = "connectionId", IPlayerQueryRepository? playerQueryRepository = null)
    {
        var sut = new CrewJoinerSignalR(crewJoinerMock, signalRGroupService ?? new SignalRGroupServiceMock(),
            playerQueryRepository ?? new PlayerQueryRepositoryMock("Rowan"), new UserSessionMock(userId), domainLoggerMock ?? new DomainLoggerMock());
        return sut;
    }
}

public class PlayerQueryRepositoryExceptionMock<T> : IPlayerQueryRepository where T : Exception, new()
{
    public Task<Player?> GetPlayer(string playerId)
    {
        throw new T();
    }
}

public class PlayerQueryRepositoryMock(string playerName) : IPlayerQueryRepository
{
    public Task<Player?> GetPlayer(string playerId)
    {
        return Task.FromResult<Player?>(Player.Create(playerId, playerName, 2, 16));
    }
}