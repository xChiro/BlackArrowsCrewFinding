using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Commands.Disbands;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Tests.Commons.Mocks;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks;

namespace BKA.Tools.CrewFinding.Tests.Crews.Commands.Disbands;

public class CrewDisbandmentTest
{
    private const string CrewId = "12314212";
    private const string UserId = "1";

    [Fact]
    public async Task Attempt_To_Disband_Crew_That_Is_Not_Owned_By_Player_Should_Throw_Exception_Async()
    {
        // Arrange
        var sut = SetupSut(false);

        // Act
        var act = async () => await sut.Disband(CrewId);

        // Assert
        await act.Should().ThrowAsync<CrewDisbandException>();
    }

    [Fact]
    public async Task Disband_Crew_That_Exists_And_Is_Owned_By_Player_Should_Be_Successful_Async()
    {
        // Arrange
        var crewCommandRepositoryMock = new CrewCommandRepositoryMock();
        var sut = SetupSut(true, crewCommandRepositoryMock);

        // Act
        await sut.Disband(CrewId);

        // Assert
        crewCommandRepositoryMock.DisbandedCrewId.Should().Be(CrewId);
    }

    private static CrewDisbandment SetupSut(bool playerIsOwner, CrewCommandRepositoryMock? crewCommandRepositoryMock = null)
    {
        ICrewValidationRepository crewValidationRepository = new CrewValidationRepositoryMock(playerOwnedCrew: playerIsOwner);
        ICrewCommandRepository crewCommandRepository = crewCommandRepositoryMock ?? new CrewCommandRepositoryMock();
        var userSession = new UserSessionMock(UserId);

        CrewDisbandment sut = new CrewDisbandment(crewValidationRepository, crewCommandRepository, userSession);
        return sut;
    }
}