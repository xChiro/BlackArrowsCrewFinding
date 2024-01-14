using System;

namespace BKA.Tools.CrewFinding.Tests.CreateCrewParties;

public class CrewPartyCreatorLocationTest
{
    [Fact]
    public void When_creating_crew_party_without_location_then_uses_default_location()
    {
        // Arrange
        var expectedLocation = Location.DefaultLocation();
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 2);

        // Act
        ExecuteSut(sut, expectedLocation);

        // Assert
        createCrewPartyResultMock.StartingPlace.Should().Be(expectedLocation);
    }

    [Theory]
    [InlineData("Stanton", "Crusader", "Crusader", "Orison")]
    [InlineData("Stanton", "Crusader", "Crusader", "Port Olisar")]
    [InlineData("Stanton", "MicroTech", "MicroTech", "New Babbage")]
    [InlineData("Pyro", "Pyro", "Pyro I", "Pyro I Station")]
    public void When_creating_crew_party_with_locations_and_save_it_successfully(string system, string planetarySystem,
        string planetMoon, string place)
    {
        // Arrange
        var expectedLocation = new Location(system, planetarySystem, planetMoon, place);
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 2);

        // Act
        ExecuteSut(sut, expectedLocation);

        // Assert
        createCrewPartyResultMock.StartingPlace.Should().Be(expectedLocation);
    }

    private static void ExecuteSut(ICrewPartyCreator sut, Location expectedLocation)
    {
        sut.Create("Rowan", 4, expectedLocation, Array.Empty<string>(), Activity.Default().Value);
    }
}