using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Commons.Values.Exceptions;
using BKA.Tools.CrewFinding.Players.Exceptions;

namespace BKA.Tools.CrewFinding.BehaviourTest.Players.Steps;

[Binding]
public class PlayerCreationAssertsSteps(
    PlayerRepositoryContext playerRepositoryContext,
    PlayerResultsContext playerResultsContext)
{
    [Then(@"I should have a player profile created with UserId ""(.*)"" and StarCitizen Handle ""(.*)""")]
    public void ThenIShouldHaveAPlayerProfileCreatedWithUserIdAndStarCitizenHandle(string userId, string playerName)
    {
        var player = playerRepositoryContext.PlayerCommandRepositoryMock.StoredPlayer;
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

    [Then(@"I should see my profile details:")]
    public void ThenIShouldSeeMyProfileDetails(Table table)
    {
        var player = playerResultsContext.Player;
        
        player.Should().NotBeNull();
        player?.Id.Should().Be(table.Rows[0]["Id"]);
        player?.CitizenName.Should().Be(table.Rows[0]["Name"]);
    }

    [Then(@"I should receive an error message that the player profile does not exist")]
    public void ThenIShouldReceiveAnErrorMessageThatThePlayerProfileDoesNotExist()
    {
        ShouldReceiveErrorType<PlayerNotFoundException>();
    }

    private void ShouldReceiveErrorType<TException>() where TException : Exception
    {
        playerResultsContext.Exception.Should().BeOfType<TException>();
    }
}