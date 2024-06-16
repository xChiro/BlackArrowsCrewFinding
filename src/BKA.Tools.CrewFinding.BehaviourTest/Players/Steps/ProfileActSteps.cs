using BKA.Tools.CrewFinding.BehaviourTest.Commons.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Mocks;
using BKA.Tools.CrewFinding.Players.Commands.Updates;

namespace BKA.Tools.CrewFinding.BehaviourTest.Players.Steps;

[Binding]
public class ProfileActSteps(
    PlayerContext playerContext,
    PlayerRepositoryContext playerRepositoryContext,
    ExceptionResultContext exceptionResultContext)
{
    [When(@"the player updates their handler name to ""(.*)""")]
    public async Task WhenThePlayerUpdatesTheirHandlerNameTo(string newName)
    {
        var sut = InitializeSut();

        await sut.Update(newName);
    }

    [When(@"the player attempts to update their handler name to (.*)")]
    public async Task WhenThePlayerAttemptsToUpdateTheirHandlerNameTo(string newName)
    {
        try
        {
            var sut = InitializeSut();
            await sut.Update(newName);
        }
        catch (Exception e)
        {
            exceptionResultContext.Exception = e;   
        }
    }

    private HandleNameUpdater InitializeSut()
    {
        playerRepositoryContext.PlayerQueryRepositoryMock =
            new PlayerQueryRepositoryMock(playerContext.PlayerId, playerContext.PlayerName);

        var sut = new HandleNameUpdater(playerRepositoryContext.PlayerQueryRepositoryMock, playerRepositoryContext.PlayerCommandRepositoryMock, new UserSessionMock(playerContext.PlayerId), playerContext.MaxLength, playerContext.MinLength);

        return sut;
    }
}