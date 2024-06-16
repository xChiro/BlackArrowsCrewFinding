using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Players.Exceptions;

namespace BKA.Tools.CrewFinding.BehaviourTest.Players.Steps;

[Binding]
public class ProfileAssertSteps(PlayerRepositoryContext playerRepositoryContext, ExceptionResultContext exceptionResultContext)
{
    [Then(@"the player's handler name is ""(.*)""")]
    public void ThenThePlayersHandlerNameIs(string name)
    {
        playerRepositoryContext.PlayerCommandRepositoryMock.StoredPlayer!.CitizenName.Value.Should().Be(name);
    }

    [Then(@"the player is notified that the handler name has an invalid length")]
    public void ThenThePlayerIsNotifiedThatTheHandlerNameHasAnInvalidLength()
    {
        exceptionResultContext.Exception!.Should().BeOfType<HandlerNameLengthException>();
    }

    [Then(@"the player's handler name is not updated")]
    public void ThenThePlayersHandlerNameIsNotUpdated()
    {
        playerRepositoryContext.PlayerCommandRepositoryMock.StoredPlayer.Should().BeNull();
    }
}