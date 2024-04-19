using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.CreateRequests;
using BKA.Tools.CrewFinding.Players.Exceptions;
using BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;
using BKA.Tools.CrewFinding.Tests.Utilities;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Creations;

public class CrewCreatorTest
{
    [Theory]
    [InlineData("Rowan", "Crew of Rowan")]
    [InlineData("James", "Crew of James")]
    public async Task Create_Crew_Forms_Correct_Crew_Name(string captainName, string expectedCrewName)
    {
        // Arrange
        var captainId = Guid.NewGuid().ToString();
        var crewPartyCommandsMock = new CrewCommandsMock();
        var playerQueriesMock = new PlayerQueriesValidationMock(captainId, captainName);
        var sut = CrewCreatorInitializer.InitializeCrewPartyCreator(crewPartyCommandsMock, playerQueriesMock);

        // Act
        await ExecuteCrewCreation(sut, captainId);

        // Assert
        crewPartyCommandsMock.Name!.Value.Should().Be(expectedCrewName);
    }

    [Fact]
    public async Task Try_To_Create_A_Crew_But_Captain_Not_Was_Found()
    {
        // Arrange
        var captainId = Guid.NewGuid().ToString();
        var crewPartyCommandsMock = new CrewCommandsMock();
        var playerQueriesMock = new PlayerQueriesValidationMock(captainId, "Rowan");
        var sut = CrewCreatorInitializer.InitializeCrewPartyCreator(crewPartyCommandsMock, playerQueriesMock);

        // Act
        var act = async () => await ExecuteCrewCreation(sut, Guid.NewGuid().ToString());

        // Assert
        await act.Should().ThrowAsync<PlayerNotFoundException>();
    }
    
    [Fact]
    public async Task Create_Crew_Assigns_Captain()
    {
        // Arrange
        var captainId = Guid.NewGuid().ToString();
        const string captainName = "Rowan";

        var createCrewPartyResultMock = new CrewCommandsMock();
        var sut = CrewCreatorInitializer.InitializeCrewPartyCreator(createCrewPartyResultMock,
            captainName: captainName);

        // Act
        await ExecuteCrewCreation(sut, captainId);

        // Assert
        createCrewPartyResultMock.Captain.Should().NotBeNull();
        createCrewPartyResultMock.Captain!.CitizenName.Should().Be(captainName);
        createCrewPartyResultMock.Captain!.Id.Should().NotBeNullOrEmpty();
        createCrewPartyResultMock.Members.Should().BeEmpty();
    }

    [Fact]
    public async Task Create_Crew_With_Current_Date()
    {
        // Arrange
        var captainId = Guid.NewGuid().ToString();
        var createCrewPartyResultMock = new CrewCommandsMock();
        var sut = CrewCreatorInitializer.InitializeCrewPartyCreator(createCrewPartyResultMock);

        // Act
        await ExecuteCrewCreation(sut, captainId);

        // Assert
        createCrewPartyResultMock.CreationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public async Task Create_Crew_With_Description_Assigns_Description()
    {
        // Arrange
        var captainId = Guid.NewGuid().ToString();
        const string description = "This is a description";
        var createCrewPartyResultMock = new CrewCommandsMock();
        var crewPartyCreatorResponseMock = new CrewCreatorResponseMock();
        var sut = CrewCreatorInitializer.InitializeCrewPartyCreator(createCrewPartyResultMock);

        // Act
        await ExecuteCrewCreation(sut, captainId, 4, description, crewPartyCreatorResponseMock);

        // Assert
        createCrewPartyResultMock.Activity!.Description.Should().Be(description);
        crewPartyCreatorResponseMock.Id.Should().NotBeNullOrEmpty();
    }

    private static async Task ExecuteCrewCreation(ICrewCreator sut, string captainId)
    {
        await ExecuteCrewCreation(sut, captainId, 4, string.Empty, new CrewCreatorResponseMock());
    }

    private static async Task ExecuteCrewCreation(ICrewCreator sut, string captainId, int totalCrew,
        string description, ICrewCreatorResponse crewCreatorResponse)
    {
        await CrewCreationExecutioner.ExecuteCrewCreation(sut, crewCreatorResponse, captainId,
            Array.Empty<string>(), Location.DefaultLocation(), "Mining", totalCrew, description);
    }
}