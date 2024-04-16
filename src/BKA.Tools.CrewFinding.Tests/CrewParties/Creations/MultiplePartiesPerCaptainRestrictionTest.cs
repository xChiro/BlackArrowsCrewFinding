using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.CrewParties.CreateRequests;
using BKA.Tools.CrewFinding.CrewParties.Exceptions;
using BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;
using BKA.Tools.CrewFinding.Tests.Utilities;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Creations;

public class MultiplePartiesPerCaptainRestrictionTest
{
    [Fact]
    public async Task
        Given_a_captain_has_created_a_party_when_they_try_to_create_another_party_then_they_should_not_be_able_to()
    {
        // Arrange
        var captainId = Guid.NewGuid().ToString();
        var crewPartyQueriesMock = new CrewPartyQueriesMock(true);

        var crewPartyCreator = CrewPartyCreatorInitializer.InitializeCrewPartyCreator(crewPartyQueriesMock, 5);
        var request = new CrewPartyCreatorRequest(captainId, "Rowan", 5, Location.DefaultLocation(),
            ["EN"], "Activity");

        // Act & Assert
        await Assert.ThrowsAsync<PlayerMultiplePartiesException>(() =>
            crewPartyCreator.Create(request, new CrewPartyCreatorResponseMock()));

        // Assert
        crewPartyQueriesMock.ReceivedCaptainId.Should().Be(captainId);
    }
}