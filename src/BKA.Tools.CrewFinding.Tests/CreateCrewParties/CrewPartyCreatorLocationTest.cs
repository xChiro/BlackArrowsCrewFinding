using System;
using BKA.Tools.CrewFinding.CrewParties.Values;

namespace BKA.Tools.CrewFinding.Tests.CreateCrewParties;
public class CrewPartyCreatorLocationTest
{
    [Fact]
    public void Create_Crew_Party_Without_Location_Uses_Default()
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
    public void Create_Crew_Party_With_Location_Succeeds(string system, string planetarySystem,
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
        var request = new CrewPartyCreatorRequest("Rowan", 5, expectedLocation,
            Array.Empty<string>(), Activity.Default().Name);
        
        sut.Create(request);
    }
}