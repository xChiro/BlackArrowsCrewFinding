using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Values.Exceptions;

namespace BKA.Tools.CrewFinding.BehaviourTest.Players.Steps;

[Binding]
public class PlayerCreationAssertsSteps
{
    private readonly PlayerResultsContext _playerResultsContext;

    public PlayerCreationAssertsSteps(PlayerResultsContext playerResultsContext)
    {
        _playerResultsContext = playerResultsContext;
    }

    [Then(@"I should have a player profile created with UserId ""(.*)"" and StarCitizen Handle ""(.*)""")]
    public void ThenIShouldHaveAPlayerProfileCreatedWithUserIdAndStarCitizenHandle(string userId, string playerName)
    {
        var player = _playerResultsContext.PlayerCommandRepositoryMock.StoredPlayer;
        player.Should().NotBeNull();
        player?.Id.Should().Be(userId);
        player?.CitizenName.Should().Be(playerName);
    }

    [Then(@"I should receive an error message that the UserId cannot be empty")]
    public void ThenIShouldReceiveAnErrorMessageThatTheUserIdCannotBeEmpty()
    {
        ShouldReceiveErrorType<UserIdInvalidException>();
    }

    [Then(@"I should receive an error message that the StarCitizen Handle length is invalid")]
    public void ThenIShouldReceiveAnErrorMessageStatingThatTheStarCitizenHandleMustBeBetweenAndCharactersInLength()
    {
        ShouldReceiveErrorType<CitizenNameLengthException>();
    }

    [Then(@"I should receive an error message that the StarCitizen Handle cannot be empty")]
    public void ThenIShouldReceiveAnErrorMessageThatTheStarCitizenHandleCannotBeEmpty()
    {
        ShouldReceiveErrorType<CitizenNameEmptyException>();
    }

    private void ShouldReceiveErrorType<TException>() where TException : Exception
    {
        _playerResultsContext.Exception.Should().BeOfType<TException>();
    }
}