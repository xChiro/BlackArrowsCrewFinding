using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Crews.JoinRequests;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class JoinCrewActSteps
{
    private readonly CrewRepositoriesContext _crewContext;
    private readonly PlayerContext _playerContext;
    private readonly PlayerRepositoryContext _playerRepositoryContext;

    public JoinCrewActSteps(PlayerContext playerContext, PlayerRepositoryContext playerRepositoryContext,
        CrewRepositoriesContext crewRepositoriesContext)
    {
        _playerContext = playerContext;
        _playerRepositoryContext = playerRepositoryContext;
        _crewContext = crewRepositoriesContext;
    }

    [When(@"the player wants to join to the Crew")]
    public async Task WhenThePlayerWantsToJoinToTheCrew()
    {
        var sut = new CrewJoiner(_crewContext.CrewQueryRepositoryMocks,
            _crewContext.CrewCommandRepositoryMock, _playerRepositoryContext.PlayerQueryRepositoryMock);

        await sut.Join(_playerContext.PlayerId, _crewContext.CrewQueryRepositoryMocks.StoredCrews[0].Id);
    }

    [When(@"the player attempts to join the Crew")]
    public async Task WhenThePlayerAttemptsToJoinTheCrew()
    {
        var sut = new CrewJoiner(_crewContext.CrewQueryRepositoryMocks,
            _crewContext.CrewCommandRepositoryMock, _playerRepositoryContext.PlayerQueryRepositoryMock);

        try
        {
            await sut.Join(_playerContext.PlayerId, _crewContext.CrewQueryRepositoryMocks.StoredCrews[0].Id);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}