using BKA.Tools.CrewFinding.BehaviourTest.Globals;

namespace BKA.Tools.CrewFinding.BehaviourTest.Steps;

[Binding]
public class PlayerArrangeSteps
{
    private readonly PlayerContext _playerContext;

    public PlayerArrangeSteps(PlayerContext playerContext)
    {
        _playerContext = playerContext;
    }

    [Given(@"a player named (.*)")]
    public void When_givenAPlayerNamed(string userName)
    {
        _playerContext.PlayerId = Guid.NewGuid().ToString();
        _playerContext.PlayerName = userName;
    }
}