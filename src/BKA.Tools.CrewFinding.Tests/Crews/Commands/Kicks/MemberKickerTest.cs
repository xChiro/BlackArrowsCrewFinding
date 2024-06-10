using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Commands.Kicks;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Tests.Commons;
using BKA.Tools.CrewFinding.Tests.Commons.Mocks;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks;

namespace BKA.Tools.CrewFinding.Tests.Crews.Commands.Kicks;

public class MemberKickerTest
{
    [Fact]
    public async Task Attempt_To_Kick_A_Member_But_I_Dont_Have_A_Crew_Throws_CrewNotFoundException()
    {
        // Arrange
        var player = Player.Create("playerId", "playerName");
        var crewCommandRepositoryMock = new CrewCommandRepositoryMock();
        var sut = InitializeSutMemberKicker(player, [], crewCommandRepositoryMock);

        // Act
        var act = async () => await sut.Kick("memberId");

        // Assert
        await act.Should().ThrowAsync<NotCaptainException>();
        crewCommandRepositoryMock.Members.Should().BeEmpty();
    }

    [Fact]
    public async Task Attempt_To_Kick_A_Member_That_Is_Not_In_The_Crew_Throws_PlayerNotInCrewFoundException()
    {
        // Arrange
        var captain = Player.Create("captainId", "captainName");
        var crews = new[] {CrewBuilder.Build("creId", captain)};
        var crewCommandRepositoryMock = new CrewCommandRepositoryMock();
        var sut = InitializeSutMemberKicker(captain, crews, crewCommandRepositoryMock);

        // Act
        var act = async () => await sut.Kick("memberId");

        // Assert
        await act.Should().ThrowAsync<NoCrewMemberException>();
        crewCommandRepositoryMock.Members.Should().BeEmpty();
    }

    [Fact]
    public async Task Kick_A_Player_From_Crew_Successfully()
    {
        // Arrange
        var captain = Player.Create("captainId", "captainName");
        var member = Player.Create("memberId", "memberName");
        var removedMember = Player.Create("removedMemberId", "memberName");
        
        var crew = CrewBuilder.Build("creId", captain);
        crew.AddMember(member);
        crew.AddMember(removedMember);
        
        var crewCommandRepositoryMock = new CrewCommandRepositoryMock();
        var sut = InitializeSutMemberKicker(captain, new[] {crew}, crewCommandRepositoryMock);
        
        // Act
        await sut.Kick(removedMember.Id);
        
        // Assert
        crew.Members.Should().Contain(member);
        crew.Members.Should().NotContain(removedMember);
        crewCommandRepositoryMock.Members.Should().Contain(member);
        crewCommandRepositoryMock.Members.Should().NotContain(removedMember);
    }

    private static MemberKicker InitializeSutMemberKicker(Player captain, Crew[] crews, ICrewCommandRepository crewCommandRepository)
    {
        var sessionMock = new UserSessionMock(captain.Id);
        var crewQuery = new CrewQueryRepositoryMock(crews);
        
        var memberKicker = new MemberKicker(sessionMock, crewQuery, crewCommandRepository);
        
        return memberKicker;
    }
}