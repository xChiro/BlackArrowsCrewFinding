using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.CrewParties.Creators;
using BKA.Tools.CrewFinding.Tests.CrewPartyCreations.Mocks;
using BKA.Tools.CrewFinding.Tests.CrewPartyCreations.Utilities;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.CrewPartyCreations;

public class CrewPartyCreatorTest
{
    [Theory]
    [InlineData("Rowan", "Crew Party of Rowan")]
    [InlineData("James", "Crew Party of James")]
    public async Task Create_Crew_Party_Forms_Correct_Crew_Name(string captainName, string expectedCrewName)
    {
        // Arrange
        var createCrewPartyResultMock = CreatedCrewPartyUtilities.InitializeCreateCrewPartyResultMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        // Act
        await ExecuteCrewCreation(sut, captainName, 4);

        // Assert
        createCrewPartyResultMock.Name!.Value.Should().Be(expectedCrewName);
    }

    [Fact]
    public async Task Create_Crew_Party_Assigns_Captain()
    {
        // Arrange
        const string captain = "Rowan";

        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        // Act
        await ExecuteCrewCreation(sut, captain, 4);

        // Assert
        createCrewPartyResultMock.Captain.Should().NotBeNull();
        createCrewPartyResultMock.Captain!.Name.Should().Be(captain);
    }

    [Fact]
    public async Task Create_Crew_Party_With_Current_Date()
    {
        // Arrange
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        // Act
        await ExecuteCrewCreation(sut, "Rowan", 4);

        // Assert
        createCrewPartyResultMock.CreationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public async Task Create_Crew_Party_With_Description_Assigns_Description()
    {
        // Arrange
        const string description = "This is a description";
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var crewPartyCreatorResponseMock = new CrewPartyCreatorResponseMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        // Act
        await ExecuteCrewCreation(sut, "Rowan", 4, description, crewPartyCreatorResponseMock);

        // Assert
        createCrewPartyResultMock.Activity!.Description.Should().Be(description);
        crewPartyCreatorResponseMock.Id.Should().NotBeNullOrEmpty();
    }

    private static async Task ExecuteCrewCreation(ICrewPartyCreator sut, string captainName, int totalCrew)
    {
        await ExecuteCrewCreation(sut, captainName, totalCrew, string.Empty, new CrewPartyCreatorResponseMock());
    }

    private static async Task ExecuteCrewCreation(ICrewPartyCreator sut, string captainName, int totalCrew,
        string description, ICrewPartyCreatorResponse crewPartyCreatorResponse)
    {
        await ExecuteCrewCreationUtilities.ExecuteCrewCreation(sut, crewPartyCreatorResponse, Array.Empty<string>(),
            Location.DefaultLocation(), "Mining", captainName, totalCrew, description);
    }
}