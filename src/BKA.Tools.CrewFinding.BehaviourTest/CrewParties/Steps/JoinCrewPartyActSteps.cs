using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.CrewParties.JoinRequests;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class JoinCrewPartyActSteps
{
    private readonly CrewPartyRepositoriesContext _crewPartyContext;
    private readonly PlayerContext _playerContext;
    private readonly PlayerRepositoryContext _playerRepositoryContext;

    public JoinCrewPartyActSteps(PlayerContext playerContext, PlayerRepositoryContext playerRepositoryContext,
        CrewPartyRepositoriesContext crewPartyRepositoriesContext)
    {
        _playerContext = playerContext;
        _playerRepositoryContext = playerRepositoryContext;
        _crewPartyContext = crewPartyRepositoriesContext;
    }

    [When(@"the player wants to join to the CrewParty")]
    public async Task WhenThePlayerWantsToJoinToTheCrewParty()
    {
        var sut = new PlayerPartyJoiner(_crewPartyContext.CrewPartyQueriesMocks,
            _crewPartyContext.CrewPartyCommandsMock, _playerRepositoryContext.PlayerQueriesMock);

        await sut.Join(_playerContext.PlayerId, _crewPartyContext.CrewPartyQueriesMocks.StoredCrewParties[0].Id);
    }

    [When(@"the player attempts to join the Crew Party")]
    public void WhenThePlayerAttemptsToJoinTheCrewParty()
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"the player attempts to join another Crew Party")]
    public void WhenThePlayerAttemptsToJoinAnotherCrewParty()
    {
        ScenarioContext.StepIsPending();
    }
}