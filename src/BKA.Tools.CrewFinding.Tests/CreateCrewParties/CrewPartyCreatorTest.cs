using System;

namespace BKA.Tools.CrewFinding.Tests.CreateCrewParties;

public class CrewPartyCreatorTest
{
    [Fact]
    public void When_creating_a_crew_party_with_invalid_capitan_name_then_throw_exception()
    {
        // Arrange
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        // Act
        var act = () => ExecuteCrewCreation(ref sut, string.Empty, 4);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData("Rowan", "Rowan's CrewParty")]
    [InlineData("James", "James' CrewParty")]
    public void When_creating_crew_party_then_crew_name_is_correctly_formed(string captainName, string expectedCrewName)
    {
        // Arrange
        var createCrewPartyResultMock = CreatedCrewPartyUtilities.InitializeCreateCrewPartyResultMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        // Act
        ExecuteCrewCreation(ref sut, captainName, 4);

        // Assert
        createCrewPartyResultMock.Name!.Value.Should().Be(expectedCrewName);
    }

    [Fact]
    public void When_creating_a_crew_party_with_not_activity_then_use_default_activity()
    {
        // Arrange
        var defaultActivities = Activity.Default().Value;
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        // Act
        ExecuteCrewCreation(ref sut, "Rowan", 4);

        // Assert
        createCrewPartyResultMock.Activity!.Value.Should().BeEquivalentTo(defaultActivities);
    }

    [Fact]
    public void When_creating_a_crew_party_should_be_assigned_to_the_captain()
    {
        // Arrange
        const string captain = "Rowan";

        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        // Act
        ExecuteCrewCreation(ref sut, captain, 4);

        // Assert
        createCrewPartyResultMock.Captain.Should().NotBeNull();
        createCrewPartyResultMock.Captain!.Name.Should().Be(captain);
    }

    [Fact]
    public void When_creating_a_crew_party_with_the_current_creation_date()
    {
        // Arrange
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        // Act
        ExecuteCrewCreation(ref sut, "Rowan", 4);

        // Assert
        createCrewPartyResultMock.CreationDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    private static void ExecuteCrewCreation(ref ICrewPartyCreator sut, string captainName, int totalCrew)
    {
        sut.Create(captainName, totalCrew, Location.DefaultLocation(), Array.Empty<string>());
    }
}   