using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests;

public class CrewHandlerHubTest
{
    private const string UserId = "1";
    private IHubContext<CrewHub> _hubContext;
    private readonly GroupManagerMock _groupManager;

    public CrewHandlerHubTest()
    {
        _groupManager = new GroupManagerMock();
        _hubContext = new HubContextMock<CrewHub>(_groupManager);
    }

    [Fact]
    public void When_CrewCreation_Throws_Exception_Do_Not_Create_A_SignalR_Group_And_Return_Crew()
    {
        // Arrange
        var crewCreatorSignalR = CreateCrewCreatorSignalR(_hubContext, new CrewCreatorExceptionMock<ArgumentException>());
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
        _hubContext = new HubContextMock<CrewHub>(new GroupManagerExceptionMock<ArgumentException>());
        
        var userSessionMock = new UserSessionMock(UserId);
        var crewCreatorSignalR = CreateCrewCreatorSignalR(_hubContext, new CrewCreatorMock("233", "Name"), userSessionMock, new DomainLoggerMock());
        var crewCreatorResponseMock = CreateCrewCreatorResponse();

        // Act
        await InvokeCreate(crewCreatorSignalR, crewCreatorResponseMock);

        // Assert
        crewCreatorResponseMock.Id.Should().Be("233");
        crewCreatorResponseMock.Name.Should().Be("Name");
    }

    [Fact]
    public async Task When_CrewCreation_Success_Create_a_SignalR_Group()
    {
        // Arrange
        const string crewId = "233";
        var crewCreatorSignalR = CreateCrewCreatorSignalR(_hubContext, new CrewCreatorMock(crewId, "Name"));
        var crewCreatorResponseMock = CreateCrewCreatorResponse();

        // Act
        await InvokeCreate(crewCreatorSignalR, crewCreatorResponseMock);

        // Assert
        crewCreatorResponseMock.Id.Should().Be(crewId);
        crewCreatorResponseMock.Name.Should().Be("Name");
        _groupManager.Groups.Should().ContainKey(crewId);
        _groupManager.Groups[crewId].Should().Contain(UserId);
    }

    private ICrewCreator CreateCrewCreatorSignalR(IHubContext<CrewHub> crewHubContext, 
        ICrewCreator? crewCreatorMock = null, IUserSession? userSessionMock = null, IDomainLogger? logger = null)
    {
        return new CrewCreatorSignalR(crewCreatorMock ?? new CrewCreatorMock("233", "Name"), 
            crewHubContext, userSessionMock ?? new UserSessionMock(UserId), logger ?? new DomainLoggerMock());
    }

    private CrewCreatorResponseMock CreateCrewCreatorResponse()
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