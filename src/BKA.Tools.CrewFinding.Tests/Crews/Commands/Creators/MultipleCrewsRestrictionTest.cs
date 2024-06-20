using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Tests.Crews.Commands.Creators.Utilities;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks.Creations;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks.Crews;

namespace BKA.Tools.CrewFinding.Tests.Crews.Commands.Creators;

public class MultipleCrewsRestrictionTest
{
    [Fact]
    public async Task Should_Prevent_Captain_From_Creating_Multiple_Crews()
    {
        // Arrange
        var captainId = Guid.NewGuid().ToString();
        var crewPartyQueriesMock = new CrewCommandRepositoryMock();

        var crewPartyCreator =
            CrewCreatorBuilder.Build(crewPartyQueriesMock, true, playerId: captainId);
        
        var request = new CrewCreatorRequest(5, Location.Default(), ["ES"], "Activity");

        // Act & Assert
        await Assert.ThrowsAsync<PlayerMultipleCrewsException>(() =>
            crewPartyCreator.Create(request, new CrewCreatorResponseMock()));

        // Assert
        crewPartyQueriesMock.Name.Should().BeNull();
    }
}