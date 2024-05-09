using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;

namespace BKA.Tools.CrewFinding.BehaviourTest.Players.Steps;

[Binding]
public class PlayerCreationArrangeSteps(
    PlayerContext playerContext,
    CrewRepositoriesContext crewRepositoriesContext,
    CrewContext crewContext)
{
    [Given(@"I am a player who does not have a player profile with the following UserId ""(.*)""")]
    public void GivenIAmAPlayerWhoDoesNotHaveAPlayerProfileWithTheFollowingUserId(string userId)
    {
        SetPlayerId(userId);
    }

    [Given(@"I am a player who does not have a player profile with empty UserId")]
    public void GivenIAmAPlayerWhoDoesNotHaveAPlayerProfileWithEmptyUserId()
    {
        SetPlayerId(string.Empty);
    }

    [Given(@"the StarCitizen Handle must be between ""(.*)"" and ""(.*)"" characters in length")]
    public void GivenTheStarCitizenHandleMustBeBetweenAndCharactersInLength(string minLength, string maxLength)
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

    [Given(@"I am the captain of an active Crew with id ""(.*)""")]
    public void GivenIAmTheCaptainOfAnActiveCrewWithId(string crewId)
    {
        var crew = crewContext.ToCrew(crewId, playerContext.PlayerName);
        
        crewRepositoriesContext.ValidationRepositoryMocks = new CrewValidationRepositoryMock(true, playerIsOwner: true);
        crewRepositoriesContext.QueryRepositoryMock = new CrewQueryRepositoryMock(new[] {crew}, playerContext.PlayerId);
    }
}