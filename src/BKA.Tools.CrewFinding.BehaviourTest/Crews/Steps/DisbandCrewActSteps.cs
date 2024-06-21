using BKA.Tools.CrewFinding.BehaviourTest.Commons.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Commons.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Crews.Commands.Disbands;
using BKA.Tools.CrewFinding.Crews.Commands.Expired;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class DisbandCrewActSteps(
    PlayerContext playerContext,
    CrewRepositoriesContext crewRepositoriesContext,
    ExceptionResultContext exceptionResultContext,
    SystemSettingContext systemSettingContext,
    CrewDisbandmentResponseMock crewDisbandmentResponseMock)
{
    [When(@"I disband the Crew")]
    public async Task WhenIDisbandTheCrew()
    {
        var userSessionMock = new UserSessionMock(playerContext.PlayerId);

        var sut = new CrewDisbandment(crewRepositoriesContext.QueryRepositoryMock,
            crewRepositoriesContext.CommandRepositoryMock, userSessionMock);

        await sut.Disband(crewDisbandmentResponseMock);
    }

    [When(@"I attempt to disband the Crew")]
    public async Task WhenIAttemptToDisbandTheCrew()
    {
        var userSessionMock = new UserSessionMock(playerContext.PlayerId);
        var sut = new CrewDisbandment(crewRepositoriesContext.QueryRepositoryMock,
            crewRepositoriesContext.CommandRepositoryMock, userSessionMock);

        try
        {
            await sut.Disband(crewDisbandmentResponseMock);
        }
        catch (Exception e)
        {
            exceptionResultContext.Exception = e;
        }
    }

    [When(@"the system disbands all expired crews")]
    public async Task WhenTheSystemDisbandsAllExpiredCrews()
    {
        var sut = new ExpiredCrewRemover(crewRepositoriesContext.QueryRepositoryMock,
            crewRepositoriesContext.CommandRepositoryMock, 
            systemSettingContext.LeastCrewTimeThreshold);

        await sut.Remove();
    }
}