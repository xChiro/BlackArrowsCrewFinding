using System.Threading.Tasks;
using BKA.Tools.CrewFinding.CrewParties.Creators;
using BKA.Tools.CrewFinding.Tests.CrewPartyCreations.Mocks;
using BKA.Tools.CrewFinding.Tests.CrewPartyCreations.Utilities;
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
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        // Act
        await ExecuteCrewCreation(sut, "Rowan", 4, defaultActivities);

        // Assert
        createCrewPartyResultMock.Activity!.Name.Should().BeEquivalentTo(defaultActivities);
    }

    [Fact]
    public async Task Create_Crew_Party_With_Activity_Succeeds()
    {
        // Arrange
        const string activity = "Mining";
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        // Act
        await ExecuteCrewCreation(sut, "Rowan", 4, activity);

        // Assert
        createCrewPartyResultMock.Activity!.Name.Should().BeEquivalentTo(activity);
    }

    private static async Task ExecuteCrewCreation(ICrewPartyCreator sut, string captainName, int totalCrew,
        string activity)
    {
         await ExecuteCrewCreationUtilities.ExecuteCrewCreation(sut, captainName, totalCrew, activity);
    }
}