using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Mocks;
using BKA.Tools.CrewFinding.Players.Commands.Creation;
using BKA.Tools.CrewFinding.Players.Queries.PlayerProfiles;

namespace BKA.Tools.CrewFinding.BehaviourTest.Players.Steps;

[Binding]
public class PlayerCreationActSteps(
    PlayerContext playerContext,
    PlayerRepositoryContext playerRepositoryContext,
    CrewRepositoriesContext crewRepositoriesContext,
    PlayerResultsContext playerResultsContext)
{
    [When(@"I attempt to create a new player profile with StarCitizen Handle ""(.*)""")]
    public void WhenIAttemptToCreateANewPlayerProfileWithUserIdAndStarCitizenHandle(string userName)
    {
        ExecutePlayerCreation(playerContext.PlayerId, userName);
    }

    [When(@"I attempt to create a new player profile with a StarCitizen Handle ""(.*)""")]
    public void WhenIAttemptToCreateANewPlayerProfileWithAStarCitizenHandle(string playerName)
    {
        ExecutePlayerCreation(string.Empty, playerName);
    }

    [When(@"I attempt to create a new player profile with an empty StarCitizen Handle")]
    public void WhenIAttemptToCreateANewPlayerProfileWithAnEmptyStarCitizenHandle()
    {
        ExecutePlayerCreation(playerContext.PlayerId, string.Empty);
    }

    [When(@"I attempt to create a new player profile with UserId ""(.*)"" and StarCitizen Handle ""(.*)""")]
    public void WhenIAttemptToCreateANewPlayerProfileWithUserIdAndStarCitizenHandle(string userId, string playerName)
    {
        ExecutePlayerCreation(userId, playerName, playerContext.MinLength, playerContext.MaxLength);
    }

    private void ExecutePlayerCreation(string userId, string starCitizenHandle, int minLength = 3, int maxLength = 30)
    {
        playerRepositoryContext.PlayerCommandRepositoryMock = new PlayerCommandRepositoryMock(minLength, maxLength);
        var sut = new PlayerCreator(playerRepositoryContext.PlayerCommandRepositoryMock, minLength, maxLength);

        try
        {
            sut.Create(userId, starCitizenHandle);
        }
        catch (Exception ex)
        {
            playerResultsContext.Exception = ex;
        }
    }

    [When(@"I get my profile")]
    public async Task WhenIGetMyProfile()
    {
        var sut = new ProfileViewer(playerRepositoryContext.PlayerQueryRepositoryMock,
            crewRepositoriesContext.QueryRepositoryMock);

        await sut.View(playerContext.PlayerId, new ProfileResponseMock(playerResultsContext));
    }

    [When(@"I attempt get my profile")]
    public async Task WhenIAttemptGetMyProfile()
    {
        try
        {
            var sut = new ProfileViewer(playerRepositoryContext.PlayerQueryRepositoryMock,
                crewRepositoriesContext.QueryRepositoryMock);
            await sut.View(playerContext.PlayerId, new ProfileResponseMock(playerResultsContext));
        }
        catch (Exception e)
        {
            playerResultsContext.Exception = e;
        }
    }
}