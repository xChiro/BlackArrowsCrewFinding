using System;
using BKA.Tools.CrewFinding.CrewParties.Values;
using BKA.Tools.CrewFinding.CrewParties.Values.Exceptions;

namespace BKA.Tools.CrewFinding.Tests.CreateCrewParties.Values;

public class CaptainTest
{
    [Fact]
    public void Create_Crew_Party_With_Invalid_Captain_Name_Throws_Exception()
    {
        // Arrange
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        // Act
        var act = () => ExecuteCrewCreation(ref sut, string.Empty);

        // Assert
        act.Should().Throw<CaptainNameEmptyException>();
    }
    
    [Theory]
    [InlineData("This is a very long name that is not valid")]
    [InlineData("This is an other very long name that is not valid")]
    public void Try_To_Create_A_Captain_with_An_Invalid_Length_Then_Throw_An_Exception(string invalidName)
    {
        // Act
        var act = () => new Captain(invalidName);

        // Assert
        act.Should().Throw<CaptainNameLengthException>();
    }

    private static void ExecuteCrewCreation(ref ICrewPartyCreator sut, string captainName)
    {
        var request = new CrewPartyCreatorRequest(captainName, 4, Location.DefaultLocation(),
            Array.Empty<string>(), Activity.Default().Name, "This is a description");

        sut.Create(request);
    }
}