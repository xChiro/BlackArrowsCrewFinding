using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Cultures.Exceptions;
using BKA.Tools.CrewFinding.Tests.CreateCrewParties.Mocks;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.CreateCrewParties;

public class MultiplePartiesPerCaptainRestrictionTest
{
    [Fact]
    public async Task Given_a_captain_has_created_a_party_when_they_try_to_create_another_party_then_they_should_not_be_able_to()
    {
        // Arrange
        const string captainName = "Rowan";
        var crewPartyQueriesMock = new CrewPartyQueriesMock(true);

        var crewPartyCreator = CreatedCrewPartyUtilities.InitializeCrewPartyCreator(crewPartyQueriesMock, 5);
        var request = new CrewPartyCreatorRequest(captainName, 5, Location.DefaultLocation(), ["EN"], "Activity");

        // Act & Assert
        await Assert.ThrowsAsync<CaptainMultiplePartiesException>(() => crewPartyCreator.Create(request));

        // Assert
        crewPartyQueriesMock.ReceivedCaptainName.Should().Be(captainName);
    }
}