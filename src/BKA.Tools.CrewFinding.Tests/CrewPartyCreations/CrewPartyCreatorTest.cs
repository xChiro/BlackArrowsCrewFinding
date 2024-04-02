using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.CrewParties.Creators;
using BKA.Tools.CrewFinding.Tests.CrewPartyCreations.Mocks;
using BKA.Tools.CrewFinding.Tests.CrewPartyCreations.Utilities;
using BKA.Tools.CrewFinding.Values;
using BKA.Tools.CrewFinding.Values.Exceptions;

namespace BKA.Tools.CrewFinding.Tests.CrewPartyCreations;

public class CrewPartyCreatorTest
{
    [Theory]
    [InlineData("Rowan", "Crew Party of Rowan")]
    [InlineData("James", "Crew Party of James")]
    public async Task Create_Crew_Party_Forms_Correct_Crew_Name(string captainName, string expectedCrewName)
    {
        // Arrange
        var captainId = Guid.NewGuid().ToString();
        var crewPartyCommandsMock = new CrewPartyCommandsMock();
        var playerQueriesMock = new PlayerQueriesValidationMock(captainId, captainName);
        var sut = CreatedCrewPartyInitializer.InitializeCrewPartyCreator(crewPartyCommandsMock, playerQueriesMock);

        // Act
        await ExecuteCrewCreation(sut, captainId);

        // Assert
        crewPartyCommandsMock.Name!.Value.Should().Be(expectedCrewName);
    }

    [Fact]
    public async Task Try_To_Create_A_Crew_Party_But_Captain_Not_Was_Found()
    {
        // Arrange
        var captainId = Guid.NewGuid().ToString();
        var crewPartyCommandsMock = new CrewPartyCommandsMock();
        var playerQueriesMock = new PlayerQueriesValidationMock(captainId, "Rowan");
        var sut = CreatedCrewPartyInitializer.InitializeCrewPartyCreator(crewPartyCommandsMock, playerQueriesMock);

        // Act
        var act = async () => await ExecuteCrewCreation(sut, Guid.NewGuid().ToString());

        // Assert
        await act.Should().ThrowAsync<PlayerNotFoundException>();
    }
    
    [Fact]
    public async Task Create_Crew_Party_Assigns_Captain()
    {
        // Arrange
        var captainId = Guid.NewGuid().ToString();
        const string captainName = "Rowan";

        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyInitializer.InitializeCrewPartyCreator(createCrewPartyResultMock,
            captainName: captainName);

        // Act
        await ExecuteCrewCreation(sut, captainId);

        // Assert
        createCrewPartyResultMock.Captain.Should().NotBeNull();
        createCrewPartyResultMock.Captain!.Name.Should().Be(captainName);
        createCrewPartyResultMock.Captain!.Id.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Create_Crew_Party_With_Current_Date()
    {
        // Arrange
        var captainId = Guid.NewGuid().ToString();
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyInitializer.InitializeCrewPartyCreator(createCrewPartyResultMock);

        // Act
        await ExecuteCrewCreation(sut, captainId);

        // Assert
        createCrewPartyResultMock.CreationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public async Task Create_Crew_Party_With_Description_Assigns_Description()
    {
        // Arrange
        var captainId = Guid.NewGuid().ToString();
        const string description = "This is a description";
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var crewPartyCreatorResponseMock = new CrewPartyCreatorResponseMock();
        var sut = CreatedCrewPartyInitializer.InitializeCrewPartyCreator(createCrewPartyResultMock);

        // Act
        await ExecuteCrewCreation(sut, captainId, 4, description, crewPartyCreatorResponseMock);

        // Assert
        createCrewPartyResultMock.Activity!.Description.Should().Be(description);
        crewPartyCreatorResponseMock.Id.Should().NotBeNullOrEmpty();
    }

    private static async Task ExecuteCrewCreation(ICrewPartyCreator sut, string captainId)
    {
        await ExecuteCrewCreation(sut, captainId, 4, string.Empty, new CrewPartyCreatorResponseMock());
    }

    private static async Task ExecuteCrewCreation(ICrewPartyCreator sut, string captainId, int totalCrew,
        string description, ICrewPartyCreatorResponse crewPartyCreatorResponse)
    {
        await CrewCreationExecutioner.ExecuteCrewCreation(sut, crewPartyCreatorResponse, captainId,
            Array.Empty<string>(), Location.DefaultLocation(), "Mining", totalCrew, description);
    }
}