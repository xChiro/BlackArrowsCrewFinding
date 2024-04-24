using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Commons.Values;
using BKA.Tools.CrewFinding.Crews.CreateRequests;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Tests.CrewParties.Creations.Utilities;
using BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Creations;

public class MultipleCrewsRestrictionTest
{
    [Fact]
    public async Task Should_Prevent_Captain_From_Creating_Multiple_Crews()
    {
        // Arrange
        var captainId = Guid.NewGuid().ToString();
        var crewPartyQueriesMock = new CrewCommandRepositoryMock();

        var crewPartyCreator = CrewCreatorInitializer.InitializeCrewPartyCreator(crewPartyQueriesMock, 5, true);
        var request = new CrewCreatorRequest(captainId, 5, Location.DefaultLocation(), ["ES"], "Activity");

        // Act & Assert
        await Assert.ThrowsAsync<PlayerMultipleCrewsException>(() =>
            crewPartyCreator.Create(request, new CrewCreatorResponseMock()));

        // Assert
        crewPartyQueriesMock.Name.Should().BeNull();
    }
}