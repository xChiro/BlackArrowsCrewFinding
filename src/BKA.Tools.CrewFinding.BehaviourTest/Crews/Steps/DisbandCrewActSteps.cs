using BKA.Tools.CrewFinding.BehaviourTest.Commons.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Crews.Commands.Disbands;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class DisbandCrewActSteps(
    PlayerContext playerContext,
    CrewRepositoriesContext crewRepositoriesContext,
    ExceptionResultContext exceptionResultContext)
{
    [When(@"I disband the Crew")]
    public async Task WhenIDisbandTheCrew()
    {
        var userSessionMock = new UserSessionMock(playerContext.PlayerId);

        var sut = new CrewDisbandment(crewRepositoriesContext.QueryRepositoryMock,
            crewRepositoriesContext.CommandRepositoryMock, userSessionMock);

        await sut.Disband(crewRepositoriesContext.QueryRepositoryMock.StoredCrews[0].Id);
    }

    [When(@"I attempt to disband the Crew")]
    public async Task WhenIAttemptToDisbandTheCrew()
    {
        var userSessionMock = new UserSessionMock(playerContext.PlayerId);

        var sut = new CrewDisbandment(crewRepositoriesContext.QueryRepositoryMock,
            crewRepositoriesContext.CommandRepositoryMock, userSessionMock);

        try
        {
            await sut.Disband(crewRepositoriesContext.QueryRepositoryMock.StoredCrews.FirstOrDefault()?.Id ??
                              string.Empty);
        }
        catch (Exception e)
        {
            exceptionResultContext.Exception = e;
        }
    }
}