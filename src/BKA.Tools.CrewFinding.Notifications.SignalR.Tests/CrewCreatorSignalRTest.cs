using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks;
using BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.Crews;
using BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.SignalR;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests;

public class CrewCreatorSignalR
{
    [Fact]
    public void When_CrewCreation_Throws_Exception_Do_Not_Create_A_SignalR_Group_And_Return_Crew()
    {
        // Arrange
        var crewCreatorSignalR = CreateCrewCreatorSignalR(new SignalRGroupServiceMock(), Guid.NewGuid().ToString(),
            new CrewCreatorExceptionMock<ArgumentException>());
        var crewCreatorResponseMock = CreateCrewCreatorResponse();

        // Act
        var act = async () => await InvokeCreate(crewCreatorSignalR, crewCreatorResponseMock);

        // Assert
        act.Should().ThrowAsync<Exception>();
        crewCreatorResponseMock.Id.Should().BeEmpty();
        crewCreatorResponseMock.Name.Should().BeEmpty();
    }

    [Fact]
    public async Task When_SignalR_Fails_Return_Success()
    {
        // Arrange
        const string crewId = "233";
        const string crewName = "Name";
        const string userId = "1";
        var exceptionHubContext = new SignalRGroupServiceExceptionMock<ArgumentException>();
        var userSessionMock = new SignalRUserSessionMock(userId);
        var crewCreatorSignalR = CreateCrewCreatorSignalR(exceptionHubContext, Guid.NewGuid().ToString(),
            new CrewCreatorMock(crewId, crewName),
            userSessionMock, new DomainLoggerMock());
        var crewCreatorResponseMock = CreateCrewCreatorResponse();

        // Act
        await InvokeCreate(crewCreatorSignalR, crewCreatorResponseMock);

        // Assert
        crewCreatorResponseMock.Id.Should().Be(crewId);
        crewCreatorResponseMock.Name.Should().Be(crewName);
        exceptionHubContext.AddConnectionIdToGroupCalls.Should().Be(1);
    }

    [Fact]
    public async Task When_CrewCreation_Success_Create_a_SignalR_Group()
    {
        // Arrange
        const string crewId = "233";
        const string crewName = "Name";
        var userId = Guid.NewGuid().ToString();
        var signalRGroupServiceMock = new SignalRGroupServiceMock();
        var crewCreatorSignalR =
            CreateCrewCreatorSignalR(signalRGroupServiceMock, userId.ToString(),
                new CrewCreatorMock(crewId, crewName));
        var crewCreatorResponseMock = CreateCrewCreatorResponse();

        // Act
        await InvokeCreate(crewCreatorSignalR, crewCreatorResponseMock);

        // Assert
        crewCreatorResponseMock.Id.Should().Be(crewId);
        crewCreatorResponseMock.Name.Should().Be(crewName);
        signalRGroupServiceMock.AddedConnectionId.Should().Be(userId);
        signalRGroupServiceMock.GroupName.Should().Be(crewId);
    }

    private static ICrewCreator CreateCrewCreatorSignalR(ISignalRGroupService crewHubContext, string userId,
        ICrewCreator? crewCreatorMock = null, ISignalRUserSession? userSessionMock = null, IDomainLogger? logger = null)
    {
        return new Notifications.SignalR.CrewCreatorSignalR(crewCreatorMock ?? new CrewCreatorMock("233", "Name"),
            crewHubContext, userSessionMock ?? new SignalRUserSessionMock(userId), logger ?? new DomainLoggerMock());
    }

    private static CrewCreatorResponseMock CreateCrewCreatorResponse()
    {
        return new CrewCreatorResponseMock();
    }

    private static async Task InvokeCreate(ICrewCreator crewCreator, ICrewCreatorResponse crewCreatorResponse)
    {
        await crewCreator.Create(
            new CrewCreatorRequest(1, Location.Default(), ["EN"], Activity.Default().ToString(), ""),
            crewCreatorResponse);
    }
}