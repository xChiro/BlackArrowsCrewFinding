using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Notifications.SignalR.Profiles;
using BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks;
using BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.Profiles;
using BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.SignalR;
using BKA.Tools.CrewFinding.Players.Queries.PlayerProfiles;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests;

public class PlayerProfileViewerSignalRTest
{
    [Fact]
    public async void Attempt_To_ViewProfile_But_PlayerProfileViewer_Fails_Should_ThrowException()
    {
        // Arrange
        var playerProfileViewerMock = new ProfileViewerExceptionMock<KeyNotFoundException>();
        var playerProfileResponseMock = new ProfileResponseMock();
        var sut = CreateSutProfileViewerSignalR(playerProfileViewerMock);

        // Act
        var action = async () => await sut.View("playerId", playerProfileResponseMock);

        // Assert
        await action.Should().ThrowAsync<KeyNotFoundException>();
        playerProfileResponseMock.Player.Should().BeNull();
        playerProfileResponseMock.ActiveCrewId.Should().BeEmpty();
        playerProfileResponseMock.ActiveCrewName.Should().BeEmpty();
    }

    [Fact]
    public async void When_ViewProfile_Should_But_SignalR_Fails_Should_CompletedOperation()
    {
        // Arrange
        const string playerId = "playerId";
        const string citizenName = "Rowan";
        const string crewId = "crewId";
        const string crewName = "crewName";
        var playerProfileViewerMock = new PlayerProfileMock(playerId, citizenName, crewId, crewName);

        var playerProfileResponseMock = new ProfileResponseMock();
        var sut = CreateSutProfileViewerSignalR(playerProfileViewerMock);

        // Act
        await sut.View(playerId, playerProfileResponseMock);

        // Assert
        ProfileResponseShouldBeNotNullOrEmpty(playerProfileResponseMock, playerId, citizenName, crewId, crewName);
    }

    [Fact]
    public async void When_ViewProfile_Should_But_SignalR_Fails_Should_CompletedOperation_And_LoggSignalError()
    {
        // Arrange
        const string playerId = "playerId";
        const string citizenName = "Rowan";
        const string crewId = "crewId";
        const string crewName = "crewName";
        var playerProfileViewerMock = new PlayerProfileMock(playerId, citizenName, crewId, crewName);

        var playerProfileResponseMock = new ProfileResponseMock();
        var signalRGroupServiceMock = new SignalRGroupServiceExceptionMock<ArgumentException>();
        var domainLoggerMock = new DomainLoggerMock();
        var sut = CreateSutProfileViewerSignalR(playerProfileViewerMock, signalRGroupServiceMock, domainLoggerMock);

        // Act
        await sut.View(playerId, playerProfileResponseMock);

        // Assert
        ProfileResponseShouldBeNotNullOrEmpty(playerProfileResponseMock, playerId, citizenName, crewId, crewName);
        domainLoggerMock.LastMessage.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async void When_ViewProfile_UserWithoutCrews_Should_Not_Be_Added_To_SignalRGroup()
    {
        // Arrange
        const string playerId = "playerId";
        const string citizenName = "Rowan";
        const string crewId = "";
        const string crewName = "";
        var playerProfileViewerMock = new PlayerProfileMock(playerId, citizenName, crewId, crewName);

        var playerProfileResponseMock = new ProfileResponseMock();
        var signalRGroupServiceMock = new SignalRGroupServiceMock();
        var sut = CreateSutProfileViewerSignalR(playerProfileViewerMock, signalRGroupServiceMock);

        // Act
        await sut.View(playerId, playerProfileResponseMock);

        // Assert
        ProfileResponseShouldBeNotNullOrEmpty(playerProfileResponseMock, playerId, citizenName, crewId, crewName);
        signalRGroupServiceMock.UserId.Should().BeEmpty();
        signalRGroupServiceMock.GroupName.Should().BeEmpty();
    }


    [Fact]
    public async void When_VewProfile_UserWithCrews_Should_Be_Added_To_SignalRGroup()
    {
        // Arrange
        const string playerId = "playerId";
        const string citizenName = "Rowan";
        const string crewId = "crewId";
        const string crewName = "crewName";
        var playerProfileViewerMock = new PlayerProfileMock(playerId, citizenName, crewId, crewName);

        var playerProfileResponseMock = new ProfileResponseMock();
        var signalRGroupServiceMock = new SignalRGroupServiceMock();
        var sut = CreateSutProfileViewerSignalR(playerProfileViewerMock, signalRGroupServiceMock);

        // Act
        await sut.View(playerId, playerProfileResponseMock);

        // Assert
        ProfileResponseShouldBeNotNullOrEmpty(playerProfileResponseMock, playerId, citizenName, crewId, crewName);
        signalRGroupServiceMock.UserId.Should().Be(playerId);
        signalRGroupServiceMock.GroupName.Should().Be(crewId);
    }

    private static ProfileViewerSignalR CreateSutProfileViewerSignalR(IProfileViewer playerProfileViewer,
        ISignalRGroupService? signalRGroupService = null, IDomainLogger? domainLogger = null)
    {
        var sut = new ProfileViewerSignalR(playerProfileViewer, signalRGroupService ?? new SignalRGroupServiceMock(),
            domainLogger ?? new DomainLoggerMock());
        return sut;
    }

    private static void ProfileResponseShouldBeNotNullOrEmpty(ProfileResponseMock profileResponseMock,
        string playerId, string citizenName, string crewId, string crewName)
    {
        profileResponseMock.Player.Should().NotBeNull();
        profileResponseMock.Player?.Id.Should().Be(playerId);
        profileResponseMock.Player?.CitizenName.Value.Should().Be(citizenName);
        profileResponseMock.ActiveCrewId.Should().Be(crewId);
        profileResponseMock.ActiveCrewName.Should().Be(crewName);
    }
}