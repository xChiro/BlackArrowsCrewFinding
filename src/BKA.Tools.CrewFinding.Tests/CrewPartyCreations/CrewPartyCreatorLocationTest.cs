using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.CrewParties.Creators;
using BKA.Tools.CrewFinding.Tests.CrewPartyCreations.Mocks;
using BKA.Tools.CrewFinding.Tests.Utilities;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.CrewPartyCreations;

public class CrewPartyCreatorLocationTest
{
    [Fact]
    public async Task Create_Crew_Party_Without_Location_Uses_Default()
    {
        // Arrange
        var expectedLocation = Location.DefaultLocation();
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CrewPartyCreatorInitializer.InitializeCrewPartyCreator(createCrewPartyResultMock, 2);

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
    public async Task Create_Crew_Party_With_Location_Succeeds(string system, string planetarySystem,
        string planetMoon, string place)
    {
        // Arrange
        var expectedLocation = new Location(system, planetarySystem, planetMoon, place);
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CrewPartyCreatorInitializer.InitializeCrewPartyCreator(createCrewPartyResultMock, 2);

        // Act
        await ExecuteSut(sut, expectedLocation);

        // Assert
        createCrewPartyResultMock.StartingPlace.Should().Be(expectedLocation);
    }

    private static async Task ExecuteSut(ICrewPartyCreator sut, Location expectedLocation)
    {   
        await CrewCreationExecutioner.ExecuteCrewCreation(sut, Guid.NewGuid().ToString(), 2, "Mining", expectedLocation);
    }
}