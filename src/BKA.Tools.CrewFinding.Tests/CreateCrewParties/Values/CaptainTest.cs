using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Tests.CreateCrewParties.Mocks;
using BKA.Tools.CrewFinding.Values;
using BKA.Tools.CrewFinding.Values.Exceptions;

namespace BKA.Tools.CrewFinding.Tests.CreateCrewParties.Values;

public class CaptainTest
{
    [Fact]
    public async void Create_Crew_Party_With_Invalid_Captain_Name_Throws_Exception()
    {
        // Arrange
        var createCrewPartyResultMock = new CrewPartyCommandsMock();
        var sut = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(createCrewPartyResultMock, 4);

        // Act & Assert
        await Assert.ThrowsAsync<CaptainNameEmptyException>(() => ExecuteCrewCreation(sut, ""));
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

    private static async Task ExecuteCrewCreation(ICrewPartyCreator sut, string captainName)
    {
        var request = new CrewPartyCreatorRequest(captainName, 4, Location.DefaultLocation(),
            Array.Empty<string>(), Activity.Default().Name, "This is a description");

        await sut.Create(request);
    }
}