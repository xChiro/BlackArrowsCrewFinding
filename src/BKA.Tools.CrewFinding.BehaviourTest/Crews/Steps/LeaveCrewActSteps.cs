using BKA.Tools.CrewFinding.BehaviourTest.Commons.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Crews.Commands.Leave;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class LeaveCrewActSteps(
    PlayerContext playerContext,
    CrewLeaverResponseMock crewLeaverResponseMock,
    CrewRepositoriesContext crewRepositoriesContext,
    ExceptionResultContext exceptionResultContext)
{
    private CrewLeaver CreateCrewLeaver()
    {
        return new CrewLeaver(crewRepositoriesContext.QueryRepositoryMock,
            crewRepositoriesContext.CommandRepositoryMock, new UserSessionMock(playerContext.PlayerId));
    }

    private async Task ExecuteLeaveRequest(Func<Task> action)
    {
        try
        {
            await action.Invoke();
        }
        catch (Exception e)
        {
            exceptionResultContext.Exception = e;
        }
    }

    [When("the player requests to leave the Crew")]
    public Task WhenThePlayerRequestsToLeaveTheCrew()
    {
        var sut = CreateCrewLeaver();
        return ExecuteLeaveRequest(() => sut.Leave(crewLeaverResponseMock));
    }

    [When(@"the player attempts to leave the Crew")]
    public Task WhenThePlayerAttemptsToLeaveTheCrew()
    {
        var sut = CreateCrewLeaver();
        return ExecuteLeaveRequest(() => sut.Leave(crewLeaverResponseMock));
    }

    [When(@"the captain attempts to leave the Crew")]
    public Task WhenTheCaptainAttemptsToLeaveTheCrew()
    {
        var sut = CreateCrewLeaver();
        return ExecuteLeaveRequest(() => sut.Leave(crewLeaverResponseMock));
    }

    [When(@"the player attempts to leave from non-existent Crew")]
    public Task WhenThePlayerAttemptsToLeaveFromNonExistentCrew()
    {
        var sut = CreateCrewLeaver();
        return ExecuteLeaveRequest(() => sut.Leave(crewLeaverResponseMock));
    }
}