using BKA.Tools.CrewFinding.BehaviourTest.Commons.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Crews.Commands.JoinRequests;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class JoinCrewActSteps
{
    private readonly CrewRepositoriesContext _crewContext;
    private readonly PlayerContext _playerContext;
    private readonly PlayerRepositoryContext _playerRepositoryContext;
    private readonly UserSessionMock _userSessionMock;

    public JoinCrewActSteps(PlayerContext playerContext, PlayerRepositoryContext playerRepositoryContext,
        CrewRepositoriesContext crewRepositoriesContext)
    {
        _playerContext = playerContext;
        _playerRepositoryContext = playerRepositoryContext;
        _crewContext = crewRepositoriesContext;
        _userSessionMock = new UserSessionMock(_playerContext.PlayerId);
    }

    [When(@"the player wants to join to the Crew")]
    public async Task WhenThePlayerWantsToJoinToTheCrew()
    {
        var sut = new CrewJoiner(_crewContext.ValidationRepositoryMocks, _crewContext.QueryRepositoryMock,
            _crewContext.CommandRepositoryMock, _playerRepositoryContext.PlayerQueryRepositoryMock,
            _userSessionMock);

        await sut.Join(_crewContext.QueryRepositoryMock.StoredCrews[0].Id);
    }

    [When(@"the player attempts to join the Crew")]
    public async Task WhenThePlayerAttemptsToJoinTheCrew()
    {
        var sut = new CrewJoiner(_crewContext.ValidationRepositoryMocks, _crewContext.QueryRepositoryMock,
            _crewContext.CommandRepositoryMock, _playerRepositoryContext.PlayerQueryRepositoryMock, _userSessionMock);

        try
        {
            await sut.Join(_crewContext.QueryRepositoryMock.StoredCrews[0].Id);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}