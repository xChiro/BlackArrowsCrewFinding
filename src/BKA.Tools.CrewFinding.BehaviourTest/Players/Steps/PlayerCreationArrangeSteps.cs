using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;

namespace BKA.Tools.CrewFinding.BehaviourTest.Players.Steps;

[Binding]
public class PlayerCreationArrangeSteps
{
    private readonly PlayerContext _playerContext;

    public PlayerCreationArrangeSteps(PlayerContext playerContext)
    {
        _playerContext = playerContext;
    }

    [Given(@"I am a player who does not have a player profile with the following UserId ""(.*)""")]
    public void GivenIAmAPlayerWithSpecificUserId(string userId)
    {
        SetPlayerId(userId);
    }

    [Given(@"I am a player who does not have a player profile with empty UserId")]
    public void GivenIAmAPlayerWithEmptyUserId()
    {
        SetPlayerId(string.Empty);
    }

    [Given(@"the StarCitizen Handle must be between ""(.*)"" and ""(.*)"" characters in length")]
    public void GivenStarCitizenHandleLengthRequirements(string minLength, string maxLength)
    {
        SetHandleLength(minLength, maxLength);
    }

    private void SetPlayerId(string userId)
    {
        _playerContext.PlayerId = userId;
    }

    private void SetHandleLength(string minLength, string maxLength)
    {
        _playerContext.MaxLength = int.Parse(maxLength);
        _playerContext.MinLength = int.Parse(minLength);
    }
}