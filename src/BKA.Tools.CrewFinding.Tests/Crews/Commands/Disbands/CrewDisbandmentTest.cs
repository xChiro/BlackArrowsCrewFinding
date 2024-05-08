using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Commands.Disbands;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Tests.Commons;
using BKA.Tools.CrewFinding.Tests.Commons.Mocks;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks;

namespace BKA.Tools.CrewFinding.Tests.Crews.Commands.Disbands;

public class CrewDisbandmentTest
{
    private const string CrewId = "12314212";

    [Fact]
    public async Task Attempt_To_Disband_Crew_That_Is_Not_Owned_By_Player_Should_Throw_Exception_Async()
    {
        // Arrange
        var sut = SetupSut(null);

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
        var sut = SetupSut(Player.Create(Guid.NewGuid().ToString(), "Adam"), crewCommandRepositoryMock);

        // Act
        await sut.Disband(CrewId);

        // Assert
        crewCommandRepositoryMock.DisbandedCrewId.Should().Be(CrewId);
    }

    private static CrewDisbandment SetupSut(Player? player, CrewCommandRepositoryMock? crewCommandRepositoryMock = null)
    {
        var captain = player ?? Player.Create(Guid.NewGuid().ToString(), "Allan");
        var userSession = new UserSessionMock(player?.Id ?? Guid.NewGuid().ToString());
        var crew = CrewBuilder.Build(CrewId, captain);
        var crewValidationRepository = new CrewQueriesRepositoryMock(crew);
        var crewDisbandRepository = crewCommandRepositoryMock ?? new CrewCommandRepositoryMock();

        var sut = new CrewDisbandment(crewValidationRepository, crewDisbandRepository, userSession);
        return sut;
    }
}