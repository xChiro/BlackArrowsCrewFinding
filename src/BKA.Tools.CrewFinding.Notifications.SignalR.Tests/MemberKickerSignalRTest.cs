using BKA.Tools.CrewFinding.Crews.Commands.Kicks;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Notifications.SignalR.Crews;
using BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks;
using BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.Crews;
using BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.SignalR;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests;

public class MemberKickerSignalRTest
{
    [Fact]
    public async void Attempt_To_KickMember_But_MemberKicker_Fails_Should_ThrowException()
    {
        // Arrange
        var memberKickerMock = new MemberKickerExceptionMock<KeyNotFoundException>();
        var sut = CreateSutMemberKickerSignalR(memberKickerMock);

        // Act
        var action = async () => await sut.Kick("memberId", new KickMemberResponse());

        // Assert
        await action.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact]
    public async void Attempt_To_KickMember_But_SigNalR_Fails_Should_CompletedOperation_And_LogError()
    {
        // Arrange
        const string memberId = "memberId";

        var memberKickerMock = new MemberKickerMock();
        var domainLoggerMock = new DomainLoggerMock();
        var signalRGroupServiceMock = new SignalRGroupServiceExceptionMock<ArgumentException>();
        var sut = CreateSutMemberKickerSignalR(memberKickerMock, signalRGroupServiceMock,
            domainLoggerMock: domainLoggerMock);

        // Act
        await sut.Kick(memberId, new KickMemberResponse());

        // Assert
        memberKickerMock.KickedMemberId.Should().Be(memberId);
        signalRGroupServiceMock.RemoveUserFromGroupCalls.Should().Be(1);
        domainLoggerMock.LastException.Should().NotBeNull();
    }

    [Fact]
    public async void When_KickMember_Sent_Should_RemoveUser_From_SignalRGroup_And_SendMessage()
    {
        // Arrange
        const string memberId = "memberId";
        const string crewId = "2313124";
        const string connectionId = "connectionId";

        var memberKickerMock = new MemberKickerMock(crewId);
        var userSessionMock = new SignalRUserSessionMock(connectionId);
        var signalRGroupServiceMock = new SignalRGroupServiceMock();
        var sut = CreateSutMemberKickerSignalR(memberKickerMock, signalRGroupServiceMock, userSessionMock);

        // Act
        await sut.Kick(memberId, new KickMemberResponse());

        // Assert
        memberKickerMock.KickedMemberId.Should().Be(memberId);
        signalRGroupServiceMock.RemoveUserFromGroupCalls.Should().Be(1);
        signalRGroupServiceMock.RemovedConnectionId.Should().Be(connectionId);
        signalRGroupServiceMock.GroupName.Should().Be(crewId);
        signalRGroupServiceMock.Message.Should().NotBeNull();
    }

    private static MemberKickerSignalR CreateSutMemberKickerSignalR(IMemberKicker memberKickerMock,
        ISignalRGroupService? signalRGroupServiceMock = null,
        ISignalRUserSession? userSessionMock = null,
        IDomainLogger? domainLoggerMock = null)
    {
        var memberKicker = new MemberKickerSignalR(memberKickerMock, domainLoggerMock ?? new DomainLoggerMock(),
            signalRGroupServiceMock ?? new SignalRGroupServiceMock(),
            userSessionMock ?? new SignalRUserSessionMock(Guid.NewGuid().ToString()));
        return memberKicker;
    }
}