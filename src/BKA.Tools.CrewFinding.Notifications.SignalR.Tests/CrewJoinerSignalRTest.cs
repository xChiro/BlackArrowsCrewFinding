using BKA.Tools.CrewFinding.Crews.Commands.JoinRequests;
using BKA.Tools.CrewFinding.Notifications.SignalR.Crews;
using BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks;
using BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.Crews;
using BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.SignalR;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests;

public class CrewJoinerSignalRTest
{
    [Fact]
    public Task Attempt_To_Sent_JoinMessage_To_Crew_But_Joiner_Fails_Throw_Exception()
    {
        // Arrange
        var crewJoinerFailMock = new CrewJoinerFailMock<ArgumentException>();
        var signalRGroupServiceMock = new SignalRGroupServiceMock();
        var sut = CreateCrewJoinerSignalR(crewJoinerFailMock, signalRGroupServiceMock);

        // Act
        var act = async () => await sut.Join("crewId");

        // Assert
        return act.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task Attempt_To_Sent_JoinMessage_To_Crew_But_SignalR_Fails_Return_Success()
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
    public async Task Send_Join_Player_To_SignalR_Group_Successfully()
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
    public async Task Send_Player_Joined_Message_To_SignalR_Group()
    {
        // Arrange
        const string crewId = "crewId";
        const string connectionId = "12312312";
        var crewJoinerMock = new CrewJoinerMock();
        var signalRGroupServiceMock = new SignalRGroupServiceMock();
        var sut = CreateCrewJoinerSignalR(crewJoinerMock, signalRGroupServiceMock, null, connectionId);

        // Act
        await sut.Join(crewId);

        // Assert
        signalRGroupServiceMock.Message.Should().NotBeNull();
        signalRGroupServiceMock.GroupName.Should().Be(crewId);
        signalRGroupServiceMock.UserId.Should().Be(connectionId);
    }

    private static CrewJoinerSignalR CreateCrewJoinerSignalR(ICrewJoiner crewJoinerMock,
        ISignalRGroupService signalRGroupService, DomainLoggerMock? domainLoggerMock = null,
        string connectionId = "connectionId")
    {
        var sut = new CrewJoinerSignalR(crewJoinerMock, signalRGroupService, new UserSessionMock(connectionId),
            domainLoggerMock ?? new DomainLoggerMock());
        return sut;
    }
}