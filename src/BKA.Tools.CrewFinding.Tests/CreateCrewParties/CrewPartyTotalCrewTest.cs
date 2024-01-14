using System;

namespace BKA.Tools.CrewFinding.Tests.CreateCrewParties;

public class CrewPartyTotalCrewTest
{
    [Theory]
    [InlineData(-1, 2)]
    [InlineData(-2, 5)]
    [InlineData(-3, 7)]
    public void When_creating_crew_party_with_max_crew_less_or_equal_to_0_then_use_default_max_crew(int userMaxCrew,
        int expectedMaxCrew)
    {
        // Arrange
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, expectedMaxCrew);

        // Act
        ExecuteCrewCreation(ref sut, "Rowan", userMaxCrew);

        // Assert
        createCrewPartyResultMock.MaxCrewNumber!.Value.Should().Be(expectedMaxCrew);
    }

    [Theory]
    [InlineData(5, 4)]
    [InlineData(6, 2)]
    [InlineData(7, 3)]
    public void When_creating_crew_party_with_max_crew_greater_than_max_allowed_then_use_max_allowed(int userMaxCrew,
        int maxCrewAllowed)
    {
        // Arrange
        var crewPartyCommandsMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(crewPartyCommandsMock, maxCrewAllowed);

        // Act
        ExecuteCrewCreation(ref sut, "Rowan", userMaxCrew);

        // Assert
        crewPartyCommandsMock.MaxCrewNumber!.Value.Should().Be(maxCrewAllowed);
    }

    [Fact]
    public void When_creating_a_crew_party_with_a_valid_total_crew_members_successfully()
    {
        // Arrange
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        const int totalCrew = 3;

        // Act
        ExecuteCrewCreation(ref sut, "Rowan", totalCrew);

        // Assert
        createCrewPartyResultMock.MaxCrewNumber!.Value.Should().Be(totalCrew);
    }

    private static void ExecuteCrewCreation(ref ICrewPartyCreator sut, string captainName, int totalCrew)
    {
        sut.Create(captainName, totalCrew, Location.DefaultLocation(), Array.Empty<string>(), Activity.Default().Value);
    }
}