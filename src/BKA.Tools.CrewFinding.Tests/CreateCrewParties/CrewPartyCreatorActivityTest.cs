using System;

namespace BKA.Tools.CrewFinding.Tests.CreateCrewParties;

public class CrewPartyCreatorActivityTest
{
    [Fact]
    public void When_creating_a_crew_party_with_not_activity_then_use_default_activity()
    {
        // Arrange
        var defaultActivities = Activity.Default().Value;
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        // Act
        ExecuteCrewCreation(ref sut, "Rowan", 4, defaultActivities);

        // Assert
        createCrewPartyResultMock.Activity!.Value.Should().BeEquivalentTo(defaultActivities);
    }
    
    [Fact]
    public void When_creating_a_crew_party_with_an_activity_successfully()
    {
        // Arrange
        const string activity = "Mining";
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        // Act
        ExecuteCrewCreation(ref sut, "Rowan", 4, activity);

        // Assert
        createCrewPartyResultMock.Activity!.Should().BeEquivalentTo(activity);
    }
    
    
    private static void ExecuteCrewCreation(ref ICrewPartyCreator sut, string captainName, int totalCrew,
        string activity)
    {
        sut.Create(captainName, totalCrew, Location.DefaultLocation(), Array.Empty<string>(), activity);
    }
}