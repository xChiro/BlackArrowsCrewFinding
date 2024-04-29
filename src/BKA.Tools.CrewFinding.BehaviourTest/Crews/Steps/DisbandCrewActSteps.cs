using BKA.Tools.CrewFinding.BehaviourTest.Commons.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Crews.Commands.Disbands;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class DisbandCrewActSteps
{
    private readonly PlayerContext _playerContext;
    private readonly CrewRepositoriesContext _crewRepositoriesContext;
    private readonly ExceptionResultContext _exceptionResultContext;

    public DisbandCrewActSteps(PlayerContext playerContext, CrewRepositoriesContext crewRepositoriesContext,
        ExceptionResultContext exceptionResultContext)
    {
        _playerContext = playerContext;
        _crewRepositoriesContext = crewRepositoriesContext;
        _exceptionResultContext = exceptionResultContext;
    }

    [When(@"I disband the Crew")]
    public async Task WhenIDisbandTheCrew()
    {
        var userSessionMock = new UserSessionMock(_playerContext.PlayerId);

        var sut = new CrewDisbandment(_crewRepositoriesContext.ValidationRepositoryMocks,
            _crewRepositoriesContext.CommandRepositoryMock, userSessionMock);

        await sut.Disband(_crewRepositoriesContext.QueryRepositoryMock.StoredCrews[0].Id);
    }

    [When(@"I attempt to disband the Crew")]
    public async Task WhenIAttemptToDisbandTheCrew()
    {
        var userSessionMock = new UserSessionMock(_playerContext.PlayerId);

        var sut = new CrewDisbandment(_crewRepositoriesContext.ValidationRepositoryMocks,
            _crewRepositoriesContext.CommandRepositoryMock, userSessionMock);

        try
        {
            await sut.Disband(_crewRepositoriesContext.QueryRepositoryMock.StoredCrews.FirstOrDefault()?.Id ??
                              string.Empty);
        }
        catch (Exception e)
        {
            _exceptionResultContext.Exception = e;
        }
    }
}