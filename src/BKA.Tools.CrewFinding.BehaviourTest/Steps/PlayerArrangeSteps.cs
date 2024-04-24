using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.BehaviourTest.Steps;

[Binding]
public class PlayerArrangeSteps
{
    private readonly PlayerContext _playerContext;
    private readonly PlayerRepositoryContext _playerRepositoryContext;

    public PlayerArrangeSteps(PlayerContext playerContext, PlayerRepositoryContext playerRepositoryContext)
    {
        _playerContext = playerContext;
        _playerRepositoryContext = playerRepositoryContext;
    }

    [Given(@"a player named (.*)")]
    [Given(@"a player named ""(.*)""")]
    [Given(@"I am a player named ""(.*)""")]
    public void When_givenAPlayerNamed(string userName)
    {
        _playerContext.PlayerId = Guid.NewGuid().ToString();
        _playerContext.PlayerName = userName;
        
        _playerRepositoryContext.PlayerQueryRepositoryMock.Players.Add(
            Player.Create(_playerContext.PlayerId, _playerContext.PlayerName));
    }
}