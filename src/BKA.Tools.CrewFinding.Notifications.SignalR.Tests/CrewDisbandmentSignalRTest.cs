using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Commands.Disbands;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Notifications.SignalR.Crews;
using BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks;
using BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.Crews;
using BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.SignalR;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests;

public class CrewDisbandmentSignalRTest
{
    [Fact]
    public async void Attempt_To_DisbandCrew_But_DecoratorFails_Should_ThrowException()
    {
        // Arrange
        var crewDisbandmentMock = new CrewDisbandmentExceptionMock<KeyNotFoundException>();
        var sut = CreateSutCrewDisbandmentSignalR(crewDisbandmentMock);

        // Act
        var action = async () => await sut.Disband(new CrewDisbandmentResponse());

        // Assert
        await action.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async void Attempt_To_DisbandCrew_But_SigNalR_Fails_Should_CompletedOperation_And_LogError()
    {
        // Arrange
        const string crewId = "crewId";

        var decoratedMock = new CrewDisbandmentMock();
        var domainLoggerMock = new DomainLoggerMock();
        var signalRGroupServiceMock = new SignalRGroupServiceExceptionMock<ArgumentException>();
        var crewDisbandmentResponse = new CrewDisbandmentResponse();
        var sut = CreateSutCrewDisbandmentSignalR(decoratedMock, signalRGroupServiceMock, domainLoggerMock);

        // Act
        await sut.Disband(crewDisbandmentResponse);

        // Assert
        crewDisbandmentResponse.CrewId.Should().Be(crewId);
        signalRGroupServiceMock.RemoveAllFromGroupCalls.Should().Be(1);
        domainLoggerMock.LastException.Should().NotBeNull();
    }

    [Fact]
    public async void Attempt_To_DisbandCrew_Should_CompletedOperation_And_RemoveAllFromGroup_And_SendMessageToGroup()
    {
        // Arrange
        const string crewId = "crewId";
        const string userId = "userId";

        var decoratedMock = new CrewDisbandmentMock();
        var signalRGroupServiceMock = new SignalRGroupServiceMock();
        var crewDisbandmentResponse = new CrewDisbandmentResponse();
        var userSessionMock = new UserSessionMock(userId);
        var sut = CreateSutCrewDisbandmentSignalR(decoratedMock, signalRGroupServiceMock, userSession: userSessionMock);

        // Act
        await sut.Disband(crewDisbandmentResponse);

        // Assert
        crewDisbandmentResponse.CrewId.Should().Be(crewId);
        signalRGroupServiceMock.RemovedGroupName.Should().Be(crewId);
        signalRGroupServiceMock.GroupName.Should().Be(crewId);
        signalRGroupServiceMock.Message.Should().NotBeNull();
        signalRGroupServiceMock.ExcludedUserIds.Should().Contain(userId);
    }

    private static CrewDisbandmentSignalR CreateSutCrewDisbandmentSignalR(ICrewDisbandment decorated,
        ISignalRGroupService? signalRGroupService = null,
        IDomainLogger? domainLogger = null,
        IUserSession? userSession = null)
    {
        return new CrewDisbandmentSignalR(decorated, signalRGroupService ?? new SignalRGroupServiceMock(),
            userSession ?? new UserSessionMock("userId"), domainLogger ?? new DomainLoggerMock());
    }
}