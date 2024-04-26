using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Commons.Values;
using BKA.Tools.CrewFinding.Crews.Commands.CreateRequests;
using BKA.Tools.CrewFinding.Tests.Crews.Commands.Creations.Utilities;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks;

namespace BKA.Tools.CrewFinding.Tests.Crews.Commands.Creations;

public class CrewCreatorLocationTest
{
    [Fact]
    public async Task Create_Crew_Without_Location_Uses_Default()
    {
        // Arrange
        var expectedLocation = Location.DefaultLocation();
        var createCrewPartyResultMock = new CrewCommandRepositoryMock();
        var sut = CrewCreatorInitializer.InitializeCrewPartyCreator(createCrewPartyResultMock);

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
        var sut = CrewCreatorInitializer.InitializeCrewPartyCreator(createCrewPartyResultMock);

        // Act
        await ExecuteSut(sut, expectedLocation);

        // Assert
        createCrewPartyResultMock.StartingPlace.Should().Be(expectedLocation);
    }

    private static async Task ExecuteSut(ICrewCreator sut, Location expectedLocation)
    {   
        await CrewCreationExecutioner.ExecuteCrewCreation(sut, Guid.NewGuid().ToString(), 2, "Mining", expectedLocation);
    }
}