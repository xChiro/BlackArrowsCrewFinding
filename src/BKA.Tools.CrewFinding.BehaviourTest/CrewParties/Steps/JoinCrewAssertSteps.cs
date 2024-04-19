using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class JoinCrewAssertSteps
{
    private readonly CrewRepositoriesContext _crewRepositoriesContext;
    private readonly PlayerContext _playerContext;

    public JoinCrewAssertSteps(CrewRepositoriesContext crewRepositoriesContext,
        PlayerContext playerContext)
    {
        _crewRepositoriesContext = crewRepositoriesContext;
        _playerContext = playerContext;
    }

    [Then(@"the player is joined to the Crew successfully")]
    public void ThenThePlayerIsJoinedToTheCrewSuccessfully()
    {
        _crewRepositoriesContext.CrewCommandsMock.CrewPartyMembers.Should()
            .Contain(player => player.Id == _playerContext.PlayerId);
    }

    [Then(@"the player is not joined to the Crew")]
    public void ThenThePlayerIsNotJoinedToTheCrew()
    {
        ScenarioContext.StepIsPending();
    }
}