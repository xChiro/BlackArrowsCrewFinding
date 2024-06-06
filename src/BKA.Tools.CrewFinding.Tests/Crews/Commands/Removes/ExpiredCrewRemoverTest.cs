using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Commands.Expired;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Tests.Commons;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks;

namespace BKA.Tools.CrewFinding.Tests.Crews.Commands.Removes;

public class ExpiredCrewRemoverTest
{
    [Fact]
    public async Task Attempt_To_Remove_Expired_Crews_But_There_Are_No_Expired_Crews_Should_Do_Nothing()
    {
        // Arrange
        Crew[] activeCrews = [CrewBuilder.Build("1", Player.Create("1", "Adam"))];
        var crewQueryRepositoryMock = new CrewQueriesRepositoryMock(crews: activeCrews);
        var crewCommandRepositoryMock = new CrewCommandRepositoryMock();
        
        var sut = new ExpiredCrewRemover(crewQueryRepositoryMock, crewCommandRepositoryMock, 3);

        // Act
        await sut.Remove();

        // Assert
        crewCommandRepositoryMock.DisbandedCrewIds.Should().BeEmpty();
    }

    [Fact]
    public async Task Remove_Expired_Crews_Should_Return_Expired_Crews()
    {
        // Arrange
        const int hoursThreshold = 3;
        string[] crewIds = ["1", "2"];

        Crew[] activeCrews =
        [
            CrewBuilder.Build(crewIds[0], Player.Create("21312", "Adam"), DateTime.UtcNow.AddHours(-hoursThreshold)),
            CrewBuilder.Build(crewIds[1], Player.Create("2", "Rowan"), DateTime.UtcNow.AddHours(-hoursThreshold - 2)),
            CrewBuilder.Build("34", Player.Create("14", "Allan"))
        ];
        
        var crewQueryRepositoryMock = new CrewQueriesRepositoryMock(crews: activeCrews);
        var crewCommandRepositoryMock = new CrewCommandRepositoryMock();

        var sut = new ExpiredCrewRemover(crewQueryRepositoryMock, crewCommandRepositoryMock, hoursThreshold);

        // Act
        await sut.Remove();

        // Assert
        crewCommandRepositoryMock.DisbandedCrewIds.Should().BeEquivalentTo(crewIds);
    }
}