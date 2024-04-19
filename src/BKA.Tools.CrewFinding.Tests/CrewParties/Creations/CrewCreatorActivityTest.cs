using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.CreateRequests;
using BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;
using BKA.Tools.CrewFinding.Tests.Utilities;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Creations;

public class CrewCreatorActivityTest
{
    [Fact]
    public async void Create_Crew_Without_Activity_Uses_Default()
    {
        // Arrange
        var defaultActivities = Activity.Default().Name;
        var createCrewPartyResultMock = new CrewCommandsMock();
        var sut = CrewCreatorInitializer.InitializeCrewPartyCreator(createCrewPartyResultMock);

        // Act
        await ExecuteCrewCreation(sut, 4, defaultActivities);

        // Assert
        createCrewPartyResultMock.Activity!.Name.Should().BeEquivalentTo(defaultActivities);
    }

    [Fact]
    public async Task Create_Crew_With_Activity_Succeeds()
    {
        // Arrange
        const string activity = "Mining";
        var createCrewPartyResultMock = new CrewCommandsMock();
        var sut = CrewCreatorInitializer.InitializeCrewPartyCreator(createCrewPartyResultMock);

        // Act
        await ExecuteCrewCreation(sut, 4, activity);

        // Assert
        createCrewPartyResultMock.Activity!.Name.Should().BeEquivalentTo(activity);
    }

    private static async Task ExecuteCrewCreation(ICrewCreator sut, int totalCrew,
        string activity)
    {
         await CrewCreationExecutioner.ExecuteCrewCreation(sut, Guid.NewGuid().ToString(), totalCrew, activity);
    }
}