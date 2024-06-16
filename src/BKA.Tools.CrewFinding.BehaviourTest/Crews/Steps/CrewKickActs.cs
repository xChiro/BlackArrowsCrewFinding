using BKA.Tools.CrewFinding.BehaviourTest.Commons.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Crews.Commands.Kicks;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class CrewKickActs(
    CrewContext crewContext,
    ExceptionResultContext exceptionResultContext,
    PlayerContext playerContext,
    CrewRepositoriesContext crewRepositoriesContext)
{
    [When(@"I attempt to kick the player with id ""(.*)"" from the crew")]
    public async Task WhenIAttemptToKickThePlayerWithIdFromTheCrew(string playerId)
    {
        await PlayerKickFromCrew(playerId);
    }

    [When(@"I kick the player with id ""(.*)"" from the crew")]
    public async Task WhenIKickThePlayerWithIdFromTheCrew(string playerId)
    {
        await PlayerKickFromCrew(playerId);
    }

    private async Task PlayerKickFromCrew(string memberId)
    {
        var sut = new MemberKicker(new UserSessionMock(playerContext.PlayerId),
            crewRepositoriesContext.QueryRepositoryMock,
            crewRepositoriesContext.CommandRepositoryMock);
        try
        {
            await sut.Kick(memberId);
        }
        catch (Exception e)
        {
            exceptionResultContext.Exception = e;
        }
    }
}