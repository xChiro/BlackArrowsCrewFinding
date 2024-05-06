using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;

namespace BKA.Tools.CrewFinding.BehaviourTest.Players.Steps;

[Binding]
public class PlayerCreationArrangeSteps(PlayerContext playerContext)
{
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
        playerContext.PlayerId = userId;
    }

    private void SetHandleLength(string minLength, string maxLength)
    {
        playerContext.MaxLength = int.Parse(maxLength);
        playerContext.MinLength = int.Parse(minLength);
    }
}