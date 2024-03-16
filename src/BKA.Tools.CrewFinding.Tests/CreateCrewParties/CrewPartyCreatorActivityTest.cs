using System;
using BKA.Tools.CrewFinding.Tests.CreateCrewParties.Mocks;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.CreateCrewParties;

public class CrewPartyCreatorActivityTest
{
    [Fact]
    public void Create_Crew_Party_Without_Activity_Uses_Default()
    {
        // Arrange
        var defaultActivities = Activity.Default().Name;
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        // Act
        ExecuteCrewCreation(ref sut, "Rowan", 4, defaultActivities);

        // Assert
        createCrewPartyResultMock.Activity!.Name.Should().BeEquivalentTo(defaultActivities);
    }

    [Fact]
    public void Create_Crew_Party_With_Activity_Succeeds()
    {
        // Arrange
        const string activity = "Mining";
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        // Act
        ExecuteCrewCreation(ref sut, "Rowan", 4, activity);

        // Assert
        createCrewPartyResultMock.Activity!.Name.Should().BeEquivalentTo(activity);
    }

    private static void ExecuteCrewCreation(ref ICrewPartyCreator sut, string captainName, int totalCrew,
        string activity)
    {
        var request = new CrewPartyCreatorRequest(captainName, totalCrew, Location.DefaultLocation(),
            Array.Empty<string>(), activity);

        sut.Create(request);
    }
}