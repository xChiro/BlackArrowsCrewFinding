using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.CrewParties.CreateRequests;
using BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;
using BKA.Tools.CrewFinding.Tests.Utilities;
using BKA.Tools.CrewFinding.Values;
using BKA.Tools.CrewFinding.Values.Exceptions;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Creations;

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
        var sut = CrewPartyCreatorInitializer.InitializeCrewPartyCreator(crewPartyCommandsMock);

        // Act
        await ExecuteCrewCreation(sut, captainId, captainName);

        // Assert
        crewPartyCommandsMock.Name!.Value.Should().Be(expectedCrewName);
    }

    [Fact]
    public async Task Create_Crew_Party_Assigns_Captain()
    {
        // Arrange
        var captainId = Guid.NewGuid().ToString();
        const string captainName = "Rowan";

        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CrewPartyCreatorInitializer.InitializeCrewPartyCreator(createCrewPartyResultMock);

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
        const string captainName = "Rowan";
        
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CrewPartyCreatorInitializer.InitializeCrewPartyCreator(createCrewPartyResultMock);

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
        var sut = CrewPartyCreatorInitializer.InitializeCrewPartyCreator(createCrewPartyResultMock);

        // Act
        await ExecuteCrewCreation(sut, captainId, "Rowan", 4, description, crewPartyCreatorResponseMock);

        // Assert
        createCrewPartyResultMock.Activity!.Description.Should().Be(description);
        crewPartyCreatorResponseMock.Id.Should().NotBeNullOrEmpty();
    }

    private static async Task ExecuteCrewCreation(ICrewPartyCreator sut, string captainId, string captainName = "Rowan")
    {
        await ExecuteCrewCreation(sut, captainId, captainName, 4, string.Empty, new CrewPartyCreatorResponseMock());
    }

    private static async Task ExecuteCrewCreation(ICrewPartyCreator sut, string captainId, string captainName, int totalCrew,
        string description, ICrewPartyCreatorResponse crewPartyCreatorResponse)
    {
        await CrewCreationExecutioner.ExecuteCrewCreation(sut, crewPartyCreatorResponse, captainId: captainId, captainName: captainName, languages: Array.Empty<string>(), expectedLocation: Location.DefaultLocation(), activity: "Mining", totalCrew: totalCrew, description: description);
    }
}