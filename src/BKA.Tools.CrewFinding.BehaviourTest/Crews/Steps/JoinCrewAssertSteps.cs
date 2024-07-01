using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class JoinCrewAssertSteps(
    CrewRepositoriesContext crewRepositoriesContext,
    PlayerContext playerContext)
{
    [Then(@"the player is joined to the Crew successfully")]
    public void ThenThePlayerIsJoinedToTheCrewSuccessfully()
    {
        crewRepositoriesContext.CommandRepositoryMock.CrewPartyMembers.Should()
            .Contain(player => player.Id == playerContext.PlayerId);
    }

    [Then(@"the player is not joined to the Crew")]
    [Then(@"the captain is not removed from the Crew")]
    public void ThenThePlayerIsNotJoinedToTheCrew()
    {
        crewRepositoriesContext.CommandRepositoryMock.CrewPartyMembers.Should()
            .NotContain(player => playerContext.PlayerId == player.Id);
    }
}