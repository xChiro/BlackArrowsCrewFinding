using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.CreateRequests;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;
using BKA.Tools.CrewFinding.Tests.Utilities;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Creations;

public class MultipleCrewsRestrictionTest
{
    [Fact]
    public async Task Should_Prevent_Captain_From_Creating_Multiple_Crews()
    {
        // Arrange
        var captainId = Guid.NewGuid().ToString();
        var crewPartyQueriesMock = new CrewQueriesMock(true);

        var crewPartyCreator = CrewCreatorInitializer.InitializeCrewPartyCreator(crewPartyQueriesMock, 5);
        var request = new CrewCreatorRequest(captainId, 5, Location.DefaultLocation(), ["ES"], "Activity");

        // Act & Assert
        await Assert.ThrowsAsync<PlayerMultipleCrewsException>(() =>
            crewPartyCreator.Create(request, new CrewCreatorResponseMock()));

        // Assert
        crewPartyQueriesMock.ReceivedCaptainId.Should().Be(captainId);
    }
}