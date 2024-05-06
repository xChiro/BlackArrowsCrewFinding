using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Mocks;
using BKA.Tools.CrewFinding.Players.Commands.Creation;

namespace BKA.Tools.CrewFinding.BehaviourTest.Players.Steps;

[Binding]
public class PlayerCreationActSteps
{
    private readonly PlayerContext _playerContext;
    private readonly PlayerRepositoryContext _playerRepositoryContext;
    private readonly PlayerResultsContext _playerResultsContext;

    public PlayerCreationActSteps(PlayerContext playerContext, PlayerRepositoryContext playerRepositoryContext,
        PlayerResultsContext playerResultsContext)
    {
        _playerRepositoryContext = playerRepositoryContext;
        _playerResultsContext = playerResultsContext;
        _playerContext = playerContext;
    }

    [When(@"I attempt to create a new player profile with StarCitizen Handle ""(.*)""")]
    public void WhenIAttemptToCreateANewPlayerProfileWithUserIdAndStarCitizenHandle(string userName)
    {
        ExecutePlayerCreation(_playerContext.PlayerId, userName);
    }

    [When(@"I attempt to create a new player profile with a StarCitizen Handle ""(.*)""")]
    public void WhenIAttemptToCreateANewPlayerProfileWithAStarCitizenHandle(string playerName)
    {
        ExecutePlayerCreation(string.Empty, playerName);
    }

    [When(@"I attempt to create a new player profile with an empty StarCitizen Handle")]
    public void WhenIAttemptToCreateANewPlayerProfileWithAnEmptyStarCitizenHandle()
    {
        ExecutePlayerCreation(_playerContext.PlayerId, string.Empty);
    }

    [When(@"I attempt to create a new player profile with UserId ""(.*)"" and StarCitizen Handle ""(.*)""")]
    public void WhenIAttemptToCreateANewPlayerProfileWithUserIdAndStarCitizenHandle(string userId, string playerName)
    {
        ExecutePlayerCreation(userId, playerName, _playerContext.MinLength, _playerContext.MaxLength);
    }

    private void ExecutePlayerCreation(string userId, string starCitizenHandle, int minLength = 3, int maxLength = 30)
    {
        _playerRepositoryContext.PlayerCommandRepositoryMock = new PlayerCommandRepositoryMock();
        var sut = new PlayerCreator(_playerRepositoryContext.PlayerCommandRepositoryMock, minLength, maxLength);

        try
        {
            sut.Create(userId, starCitizenHandle);
        }
        catch (Exception ex)
        {
            _playerResultsContext.Exception = ex;
        }
    }
}