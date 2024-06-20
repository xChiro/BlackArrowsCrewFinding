using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;
using BKA.Tools.CrewFinding.Tests.Crews.Commands.Creators.Utilities;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks.Crews;

namespace BKA.Tools.CrewFinding.Tests.Crews.Commands.Creators;

public class CrewCreatorLocationTest
{
    [Fact]
    public async Task Create_Crew_Without_Location_Uses_Default()
    {
        // Arrange
        var expectedLocation = Location.Default();
        var createCrewPartyResultMock = new CrewCommandRepositoryMock();
        var sut = CrewCreatorBuilder.Build(createCrewPartyResultMock);

        // Act
        await ExecuteSut(sut, expectedLocation);

        // Assert
        createCrewPartyResultMock.StartingPlace.Should().Be(expectedLocation);
    }

    [Theory]
    [InlineData("Stanton", "Crusader", "Crusader", "Orison")]
    [InlineData("Stanton", "Crusader", "Crusader", "Port Olisar")]
    [InlineData("Stanton", "MicroTech", "MicroTech", "New Babbage")]
    [InlineData("Pyro", "Pyro", "Pyro I", "Pyro I Station")]
    public async Task Create_Crew_With_Location_Succeeds(string system, string planetarySystem,
        string planetMoon, string place)
    {
        // Arrange
        var expectedLocation = new Location(system, planetarySystem, planetMoon, place);
        var createCrewPartyResultMock = new CrewCommandRepositoryMock();
        var sut = CrewCreatorBuilder.Build(createCrewPartyResultMock);

        // Act
        await ExecuteSut(sut, expectedLocation);

        // Assert
        createCrewPartyResultMock.StartingPlace.Should().Be(expectedLocation);
    }

    private static async Task ExecuteSut(ICrewCreator sut, Location expectedLocation)
    {   
        await CrewCreatorExecutioner.Execute(sut, 2, "Mining", expectedLocation);
    }
}