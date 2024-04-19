using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class JoinCrewPartyAssertSteps
{
    private readonly CrewPartyRepositoriesContext _crewPartyRepositoriesContext;
    private readonly PlayerContext _playerContext;

    public JoinCrewPartyAssertSteps(CrewPartyRepositoriesContext crewPartyRepositoriesContext,
        PlayerContext playerContext)
    {
        _crewPartyRepositoriesContext = crewPartyRepositoriesContext;
        _playerContext = playerContext;
    }

    [Then(@"the player is joined to the CrewParty successfully")]
    public void ThenThePlayerIsJoinedToTheCrewPartySuccessfully()
    {
        _crewPartyRepositoriesContext.CrewPartyCommandsMock.CrewPartyMembers.Should()
            .Contain(player => player.Id == _playerContext.PlayerId);
    }

    [Then(@"the player is not joined to the Crew Party")]
    public void ThenThePlayerIsNotJoinedToTheCrewParty()
    {
        ScenarioContext.StepIsPending();
    }
}