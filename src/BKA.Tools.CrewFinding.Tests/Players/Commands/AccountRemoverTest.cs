using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;
using BKA.Tools.CrewFinding.Tests.Commons;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks.Crews;
using BKA.Tools.CrewFinding.Tests.Players.Mocks;

namespace BKA.Tools.CrewFinding.Tests.Players.Commands;

public class AccountRemoverTest
{
    private static readonly UserSessionMock UserSessionMock = new();

    [Fact]
    public async Task Attempt_To_Remove_Player_CrewsHistory_When_RemoveCrewHistory_ThrowsException()
    {
        // Arrange
        var crewCommandRepositoryMock = new CrewCommandRepositoryExceptionMock<Exception>();
        var crew = CrewBuilder.Build("23214", Player.Create(UserSessionMock.GetUserId(), "Rowan", 2, 16));
        var crewQueryRepositoryMock = new CrewQueryRepositoryMock([crew]);
        var sut = InitializeSut(crewCommandRepository: crewCommandRepositoryMock,
            crewQueryRepository: crewQueryRepositoryMock);

        // Act
        var act = async () => await sut.Remove();

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task Remove_Player_From_Active_Crew_But_Player_Does_Not_Have_An_Active_Crew_Should_Do_Nothing()
    {
        // Arrange
        var crewQueryRepositoryExceptionMock = new CrewQueryRepositoryMock();
        var sut = InitializeSut(crewQueryRepositoryExceptionMock);

        // Act
        await sut.Remove();

        // Assert
        crewQueryRepositoryExceptionMock.GetActiveCrewByPlayerIdCountCalled.Should().Be(1);
    }

    [Fact]
    public async Task Remove_ActiveCrew_When_Player_Is_Captain()
    {
        // Arrange
        var userId = UserSessionMock.GetUserId();
        var crew = CreateCrew(userId);
        var crewCommandRepositoryMock = new CrewCommandRepositoryMock();

        var sut = InitializeSut(crewCommandRepository: crewCommandRepositoryMock,
            crewQueryRepository: new CrewQueryRepositoryMock([crew]));

        // Act
        await sut.Remove();

        // Assert
        crewCommandRepositoryMock.DeletedCrewId.Should().Be(crew.Id);
    }

    [Fact]
    public async Task Remove_Player_From_Active_Crew_When_Player_Is_Not_Captain()
    {
        // Arrange
        var crew = CreateCrew("21312");
        crew.AddMember(Player.Create(UserSessionMock.GetUserId(), "Rowan", 2, 16));

        var crewCommandRepositoryMock = new CrewCommandRepositoryMock(crew);

        var sut = InitializeSut(crewCommandRepository: crewCommandRepositoryMock,
            crewQueryRepository: new CrewQueryRepositoryMock([crew]));

        // Act
        await sut.Remove();

        // Assert
        crewCommandRepositoryMock.Crew.Members.Should().NotContain(member => member.Id == UserSessionMock.GetUserId());
    }

    [Fact]
    public async Task Remove_Player_From_System_Successfully()
    {
        // Arrange
        var crew = CreateCrew(UserSessionMock.GetUserId());
        var crewCommandRepositoryMock = new CrewCommandRepositoryMock(crew);
        var playerCommandRepositoryMock = new PlayerCommandRepositoryMock();

        var sut = InitializeSut(crewCommandRepository: crewCommandRepositoryMock,
            crewQueryRepository: new CrewQueryRepositoryMock([crew]),
            playerCommandRepository: playerCommandRepositoryMock);

        // Act
        await sut.Remove();

        // Assert
        crewCommandRepositoryMock.DeletedCrewId.Should().Be(crew.Id);
        crewCommandRepositoryMock.DeletedPlayerId.Should().Be(UserSessionMock.GetUserId());
        playerCommandRepositoryMock.DeletedPlayerId.Should().Be(UserSessionMock.GetUserId());
    }

    private static Crew CreateCrew(string captainId)
    {
        var captain = Player.Create(captainId, "Allan", 2, 16);
        var crew = CrewBuilder.Build("23214", captain);
        
        return crew;
    }

    public static IAccountRemover InitializeSut(
        ICrewQueryRepository? crewQueryRepository = null,
        ICrewCommandRepository? crewCommandRepository = null,
        IPlayerCommandRepository? playerCommandRepository = null)
    {
        var queryRepositoryExceptionMock = crewQueryRepository ?? new CrewQueryRepositoryMock();
        var crewCommandRepositoryMock = crewCommandRepository ?? new CrewCommandRepositoryMock();
        var playerCommandRepositoryMock = playerCommandRepository ?? new PlayerCommandRepositoryMock();

        return new AccountRemover(queryRepositoryExceptionMock, crewCommandRepositoryMock, UserSessionMock,
            playerCommandRepositoryMock);
    }
}