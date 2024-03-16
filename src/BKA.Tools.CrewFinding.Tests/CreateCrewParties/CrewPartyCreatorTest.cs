using System;
using BKA.Tools.CrewFinding.Tests.CreateCrewParties.Mocks;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.CreateCrewParties;

public class CrewPartyCreatorTest
{
    [Theory]
    [InlineData("Rowan", "Crew Party of Rowan")]
    [InlineData("James", "Crew Party of James")]
    public void Create_Crew_Party_Forms_Correct_Crew_Name(string captainName, string expectedCrewName)
    {
        // Arrange
        var createCrewPartyResultMock = CreatedCrewPartyUtilities.InitializeCreateCrewPartyResultMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        // Act
        ExecuteCrewCreation(ref sut, captainName, 4);

        // Assert
        createCrewPartyResultMock.Name!.Value.Should().Be(expectedCrewName);
    }

    [Fact]
    public void Create_Crew_Party_Assigns_Captain()
    {
        // Arrange
        const string captain = "Rowan";

        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        // Act
        ExecuteCrewCreation(ref sut, captain, 4);

        // Assert
        createCrewPartyResultMock.Captain.Should().NotBeNull();
        createCrewPartyResultMock.Captain!.Name.Should().Be(captain);
    }

    [Fact]
    public void Create_Crew_Party_With_Current_Date()
    {
        // Arrange
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        // Act
        ExecuteCrewCreation(ref sut, "Rowan", 4);

        // Assert
        createCrewPartyResultMock.CreationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }
    
    [Fact]
    public void Create_Crew_Party_With_Description_Assigns_Description()
    {
        // Arrange
        const string description = "This is a description";
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        // Act
        ExecuteCrewCreation(ref sut, "Rowan", 4, description);

        // Assert
        createCrewPartyResultMock.Activity!.Description.Should().Be(description);
    }

    private static void ExecuteCrewCreation(ref ICrewPartyCreator sut, string captainName, int totalCrew)
    {
        ExecuteCrewCreation(ref sut, captainName, totalCrew, string.Empty);
    }

    private static void ExecuteCrewCreation(ref ICrewPartyCreator sut, string captainName, int totalCrew,
        string description)
    {
        
        var request = new CrewPartyCreatorRequest(captainName, totalCrew, Location.DefaultLocation(), 
            Array.Empty<string>(), Activity.Default().Name, description);

        sut.Create(request);
    }
}