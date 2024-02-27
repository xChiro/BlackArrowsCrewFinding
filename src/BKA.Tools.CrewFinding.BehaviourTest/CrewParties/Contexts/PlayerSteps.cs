using BKA.Tools.CrewFinding.BehaviourTest.Globals;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;

[Binding]
public class PlayerSteps
{
    private readonly PlayerContext _playerContext;

    public PlayerSteps(PlayerContext playerContext)
    {
        _playerContext = playerContext;
    }
    
    [Given(@"a player named (.*)")]
    public void When_givenAPlayerNamed(string userName)
    {
        _playerContext.UserName = userName;
    }
}