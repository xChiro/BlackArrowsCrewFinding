using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.CrewParties.Creators;
using BKA.Tools.CrewFinding.Tests.CrewPartyCreations.Mocks;
using BKA.Tools.CrewFinding.Tests.Utilities;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.CrewPartyCreations;

public class CrewPartyCreatorActivityTest
{
    [Fact]
    public async void Create_Crew_Party_Without_Activity_Uses_Default()
    {
        // Arrange
        var defaultActivities = Activity.Default().Name;
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CrewPartyCreatorInitializer.InitializeCrewPartyCreator(createCrewPartyResultMock);

        // Act
        await ExecuteCrewCreation(sut, 4, defaultActivities);

        // Assert
        createCrewPartyResultMock.Activity!.Name.Should().BeEquivalentTo(defaultActivities);
    }

    [Fact]
    public async Task Create_Crew_Party_With_Activity_Succeeds()
    {
        // Arrange
        const string activity = "Mining";
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CrewPartyCreatorInitializer.InitializeCrewPartyCreator(createCrewPartyResultMock);

        // Act
        await ExecuteCrewCreation(sut, 4, activity);

        // Assert
        createCrewPartyResultMock.Activity!.Name.Should().BeEquivalentTo(activity);
    }

    private static async Task ExecuteCrewCreation(ICrewPartyCreator sut, int totalCrew,
        string activity)
    {
         await CrewCreationExecutioner.ExecuteCrewCreation(sut, Guid.NewGuid().ToString(), totalCrew, activity);
    }
}