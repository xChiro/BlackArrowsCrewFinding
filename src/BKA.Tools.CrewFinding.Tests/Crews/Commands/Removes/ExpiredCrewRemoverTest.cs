using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Commands.Expired;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Tests.Commons;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks.Crews;

namespace BKA.Tools.CrewFinding.Tests.Crews.Commands.Removes;

public class ExpiredCrewRemoverTest
{
    [Fact]
    public async Task Attempt_To_Remove_Expired_Crews_But_There_Are_No_Expired_Crews_Should_Do_Nothing()
    {
        // Arrange
        Crew[] activeCrews = [CrewBuilder.Build("1", CreatePlayer("1", "Adam"))];
        var crewQueryRepositoryMock = new CrewQueryRepositoryMock(crews: activeCrews);
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
            CrewBuilder.Build(crewIds[0], CreatePlayer("21312", "Adam"), DateTime.UtcNow.AddHours(-hoursThreshold)),
            CrewBuilder.Build(crewIds[1], CreatePlayer("2", "Rowan"), DateTime.UtcNow.AddHours(-hoursThreshold - 2)),
            CrewBuilder.Build("34", CreatePlayer("14", "Allan"))
        ];
        
        var crewQueryRepositoryMock = new CrewQueryRepositoryMock(crews: activeCrews);
        var crewCommandRepositoryMock = new CrewCommandRepositoryMock();

        var sut = new ExpiredCrewRemover(crewQueryRepositoryMock, crewCommandRepositoryMock, hoursThreshold);

        // Act
        await sut.Remove();

        // Assert
        crewCommandRepositoryMock.DisbandedCrewIds.Should().BeEquivalentTo(crewIds);
    }
    
    private static Player CreatePlayer(string playerId, string playerName = "playerName")
    {
        const int playerMinLength = 2;
        const int playerMaxLength = 16;

        return Player.Create(playerId, playerName, playerMinLength, playerMaxLength);
    }
}