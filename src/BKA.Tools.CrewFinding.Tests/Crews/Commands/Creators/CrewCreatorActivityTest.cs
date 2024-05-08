using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;
using BKA.Tools.CrewFinding.Tests.Crews.Commands.Creators.Utilities;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks;

namespace BKA.Tools.CrewFinding.Tests.Crews.Commands.Creators;

public class CrewCreatorActivityTest
{
    [Fact]
    public async void Create_Crew_Without_Activity_Uses_Default()
    {
        // Arrange
        var defaultActivities = Activity.Default().Name;
        var createCrewPartyResultMock = new CrewCommandRepositoryMock();
        var sut = CrewCreatorBuilder.Build(createCrewPartyResultMock);

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
        var createCrewPartyResultMock = new CrewCommandRepositoryMock();
        var sut = CrewCreatorBuilder.Build(createCrewPartyResultMock);

        // Act
        await ExecuteCrewCreation(sut, 4, activity);

        // Assert
        createCrewPartyResultMock.Activity!.Name.Should().BeEquivalentTo(activity);
    }

    private static async Task ExecuteCrewCreation(ICrewCreator sut, int totalCrew,
        string activity)
    {
         await CrewCreatorExecutioner.Execute(sut, totalCrew, activity);
    }
}