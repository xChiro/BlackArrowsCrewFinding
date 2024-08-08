using BKA.Tools.CrewFinding.Crews.Commands.Leave;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Notifications.SignalR.Crews;
using BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks;
using BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.SignalR;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests;

public class CrewLeaverSignalRTest
{
    [Fact]
    public void Attempt_LeavingCrew_An_ExceptionIsThrown_ShouldThrowAnException()
    {
        // Arrange
        var crewLeaverExceptionMock = new CrewLeaverExceptionMock<ArgumentException>();
        var sut = CreateSutCrewLeaverSignalR(crewLeaverExceptionMock);
        var output = new CrewLeaverResponseMock();

        // Act
        var act = async () => await sut.Leave(new CrewLeaverResponseMock());

        // Assert
        act.Should().ThrowAsync<ArgumentException>();
        output.CrewId.Should().BeNullOrEmpty();
    }

    [Fact]
    public async void Attempt_LeavingCrew_SignalRLeaveCrew_Fails_ShouldNotThrowAnException_And_LeaveTheCrew()
    {
        // Arrange
        var signalRGroupServiceMock = CreateSignalRGroupServiceArgumentExceptionMock();
        var crewLeaverMock = new CrewLeaverMock();
        var domainLoggerMock = CreateDomainLoggerMock();
        var sut = CreateSutCrewLeaverSignalR(crewLeaverMock, "connectionId", signalRGroupServiceMock, domainLoggerMock);

        // Act
        await sut.Leave(new CrewLeaverResponseMock());

        // Assert
        crewLeaverMock.LeaveCallCount.Should().Be(1);
        signalRGroupServiceMock.RemoveUserFromGroupCalls.Should().Be(1);
        domainLoggerMock.LastException.Should().NotBeNull();
    }

    [Fact]
    public async void When_LeavingCrew_SignalRLeaveCrew_Removes_Connection_From_Group_Successfully()
    {
        // Arrange
        const string userId = "userId";
        const string crewId = "crewId";

        var signalRGroupServiceMock = new SignalRGroupServiceMock();
        var domainLoggerMock = CreateDomainLoggerMock();
        var crewLeaverMock = new CrewLeaverMock(crewId);
        var sut = CreateSutCrewLeaverSignalR(crewLeaverMock, userId, signalRGroupServiceMock, domainLoggerMock);
        var output = new CrewLeaverResponseMock();

        // Act
        await sut.Leave(output);

        // Assert
        domainLoggerMock.LastException.Should().BeNull();
        signalRGroupServiceMock.GroupName.Should().Be(crewId);
        signalRGroupServiceMock.RemovedUserId.Should().Be(userId);
        output.CrewId.Should().Be(crewId);
    }

    [Fact]
    public async void Attempt_LeavingCrew_SignalRLeaveCrew_Throws_Then_ShouldLogError_And_Perform_LeaveCrew()
    {
        // Arrange
        const string userId = "userId";
        const string crewId = "CrewId";
        
        var domainLoggerMock = new DomainLoggerMock();

        var signalRGroupServiceMock = new SignalRGroupServiceSendMessageThrowsMock<ArgumentException>();
        var sut = CreateSutCrewLeaverSignalR(new CrewLeaverMock(crewId), "userId", signalRGroupServiceMock,
            domainLoggerMock);

        // Act
        await sut.Leave(new CrewLeaverResponseMock());

        // Assert
        signalRGroupServiceMock.GroupName.Should().Be(crewId);
        signalRGroupServiceMock.UserId.Should().Be(userId);
        domainLoggerMock.LastException.Should().BeOfType<ArgumentException>();
    }
    
    [Fact]
    public async void When_LeavingCrew_SignalRLeaveCrew_Sends_Message_To_Group_Successfully()
    {
        // Arrange
        const string userId = "userId";
        const string crewId = "crewId";

        var signalRGroupServiceMock = new SignalRGroupServiceMock();
        var domainLoggerMock = CreateDomainLoggerMock();
        var crewLeaverMock = new CrewLeaverMock(crewId);
        var sut = CreateSutCrewLeaverSignalR(crewLeaverMock, userId, signalRGroupServiceMock, domainLoggerMock);
        var output = new CrewLeaverResponseMock();

        // Act
        await sut.Leave(output);

        // Assert
        domainLoggerMock.LastException.Should().BeNull();
        signalRGroupServiceMock.GroupName.Should().Be(crewId);
        signalRGroupServiceMock.Message.Should().NotBeNull();
        output.CrewId.Should().Be(crewId);
    }

    private static DomainLoggerMock CreateDomainLoggerMock() => new DomainLoggerMock();

    private static SignalRGroupServiceExceptionMock<ArgumentException> CreateSignalRGroupServiceArgumentExceptionMock()
    {
        var signalRGroupServiceMock = new SignalRGroupServiceExceptionMock<ArgumentException>();
        return signalRGroupServiceMock;
    }

    private static CrewLeaverSignalR CreateSutCrewLeaverSignalR(ICrewLeaver crewLeaverMock, string userId = "",
        ISignalRGroupService? signalRGroupServiceMock = null, IDomainLogger? domainLoggerMock = null)
    {
        var crewLeaverSignalR = new CrewLeaverSignalR(crewLeaverMock, domainLoggerMock ?? CreateDomainLoggerMock(),
            signalRGroupServiceMock ?? new SignalRGroupServiceMock(), new UserSessionMock(userId));

        return crewLeaverSignalR;
    }
}