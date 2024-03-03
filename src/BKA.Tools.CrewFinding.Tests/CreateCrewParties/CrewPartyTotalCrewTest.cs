using System;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.CreateCrewParties;

public class CrewPartyTotalCrewTest
{
    [Theory]
    [InlineData(-1, 2)]
    [InlineData(-2, 5)]
    [InlineData(-3, 7)]
    public void Create_Crew_Party_With_Negative_Max_Crew_Uses_Default(int userMaxCrew, int expectedMaxCrew)
    {
        // Arrange
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, expectedMaxCrew);

        // Act
        ExecuteCrewCreation(ref sut, "Rowan", userMaxCrew);

        // Assert
        createCrewPartyResultMock.MaxCrewNumber!.Value.Should().Be(expectedMaxCrew);
    }

    [Theory]
    [InlineData(5, 4)]
    [InlineData(6, 2)]
    [InlineData(7, 3)]
    public void Create_Crew_Party_With_Max_Crew_Exceeding_Limit_Uses_Max_Allowed(int userMaxCrew, int maxCrewAllowed)
    {
        // Arrange
        var crewPartyCommandsMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(crewPartyCommandsMock, maxCrewAllowed);

        // Act
        ExecuteCrewCreation(ref sut, "Rowan", userMaxCrew);

        // Assert
        crewPartyCommandsMock.MaxCrewNumber!.Value.Should().Be(maxCrewAllowed);
    }

    [Fact]
    public void Create_Crew_Party_With_Valid_Total_Crew_Succeeds()
    {
        // Arrange
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        const int totalCrew = 3;

        // Act
        ExecuteCrewCreation(ref sut, "Rowan", totalCrew);

        // Assert
        createCrewPartyResultMock.MaxCrewNumber!.Value.Should().Be(totalCrew);
    }

    private static void ExecuteCrewCreation(ref ICrewPartyCreator sut, string captainName, int totalCrew)
    {
        var request = new CrewPartyCreatorRequest(captainName, totalCrew, Location.DefaultLocation(), 
            Array.Empty<string>(), Activity.Default().Name);
        
        sut.Create(request);
    }
}