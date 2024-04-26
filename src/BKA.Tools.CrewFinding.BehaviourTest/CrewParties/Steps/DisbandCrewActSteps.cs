using BKA.Tools.CrewFinding.BehaviourTest.Commons.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Crews.Disbands;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

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

        var sut = new CrewDisbandment(_crewRepositoriesContext.CrewValidationRepositoryMocks,
            _crewRepositoriesContext.CrewCommandRepositoryMock, userSessionMock);

        await sut.Disband(_crewRepositoriesContext.CrewValidationRepositoryMocks.StoredCrews[0].Id);
    }

    [When(@"I attempt to disband the Crew")]
    public async Task WhenIAttemptToDisbandTheCrew()
    {
        var userSessionMock = new UserSessionMock(_playerContext.PlayerId);

        var sut = new CrewDisbandment(_crewRepositoriesContext.CrewValidationRepositoryMocks,
            _crewRepositoriesContext.CrewCommandRepositoryMock, userSessionMock);

        try
        {
            await sut.Disband(_crewRepositoriesContext.CrewValidationRepositoryMocks.StoredCrews.FirstOrDefault()?.Id ??
                              string.Empty);
        }
        catch (Exception e)
        {
            _exceptionResultContext.Exception = e;
        }
    }
}