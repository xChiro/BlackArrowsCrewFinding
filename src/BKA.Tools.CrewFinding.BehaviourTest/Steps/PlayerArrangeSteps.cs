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
    public void When_givenAPlayerNamed(string userName)
    {
        _playerContext.PlayerId = Guid.NewGuid().ToString();
        _playerContext.UserName = userName;
        
        _playerRepositoryContext.PlayerQueriesMock.Players.Add(
            Player.Create(_playerContext.PlayerId, _playerContext.UserName));
    }
}