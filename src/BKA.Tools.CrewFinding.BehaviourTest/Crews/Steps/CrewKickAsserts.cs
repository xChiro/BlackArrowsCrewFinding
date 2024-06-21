using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.Commons.Exceptions;
using BKA.Tools.CrewFinding.Crews.Exceptions;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class CrewKickAsserts(
    ExceptionResultContext exceptionResultContext,
    CrewRepositoriesContext crewRepositoriesContext)
{
    [Then(@"I should see an error message indicating I am not the captain of the crew")]
    public void ThenIShouldSeeAnErrorMessageIndicatingIAmNotTheCaptainOfTheCrew()
    {
        exceptionResultContext.Exception.Should().BeOfType<NotCaptainException>();
    }

    [Then(@"I should see an error message indicating the player is not in the crew")]
    public void ThenIShouldSeeAnErrorMessageIndicatingThePlayerIsNotInTheCrew()
    {
        exceptionResultContext.Exception.Should().BeOfType<PlayerNotInCrewException>();
    }

    [Then(@"the player with id ""(.*)"" is no longer in the crew")]
    public void ThenThePlayerWithIdIsNoLongerInTheCrew(string removedPlayerId)
    {
        crewRepositoriesContext.CommandRepositoryMock.CrewPartyMembers.Should()
            .NotContain(player => player.Id == removedPlayerId);
    }
}