using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class CrewArrangeSteps(
    CrewContext crewContext,
    PlayerContext playerContext,
    CrewRepositoriesContext crewRepositoriesContext)
{
    [Given(@"the default MaxCrewSize is (.*)")]
    public void GivenTheDefaultMaxCrewSizeIs(string defaultMaxCrewSize)
    {
        crewContext.MaxPlayerAllowed = int.Parse(defaultMaxCrewSize);
    }

    [Given(@"the player already has an active Crew")]
    public void GivenThePlayerAlreadyHasAnActiveCrew()
    {
        var crew = crewContext.ToCrew(playerContext.PlayerId, playerContext.PlayerName);
        crewRepositoriesContext.ValidationRepositoryMocks = new CrewValidationRepositoryMock(true);
        crewRepositoriesContext.QueryRepositoryMock = new CrewQueryRepositoryMock(new[] {crew});
    }

    [Given(@"I am the captain of an active Crew")]
    public void GivenIAmTheCaptainOfAnActiveCrew()
    {
        var crew = crewContext.ToCrew(playerContext.PlayerId, playerContext.PlayerName);

        crewRepositoriesContext.ValidationRepositoryMocks =
            new CrewValidationRepositoryMock(true, playerIsOwner: true);
        crewRepositoriesContext.QueryRepositoryMock = new CrewQueryRepositoryMock(new[] {crew}, playerContext.PlayerId);
    }

    [Given(@"there is an active crew created by another player")]
    public void GivenThereIsAnActiveCrewCreatedByAnotherPlayer()
    {
        var crew = crewContext.ToCrew("anotherPlayerId", "anotherPlayerName");
        crewRepositoriesContext.ValidationRepositoryMocks = new CrewValidationRepositoryMock(true);
        crewRepositoriesContext.QueryRepositoryMock = new CrewQueryRepositoryMock(new[] {crew});
    }

    [Given(@"the player is the captain of a Crew")]
    public void GivenThePlayerIsTheCaptainOfACrew()
    {
        var crew = crewContext.ToCrew(playerContext.PlayerId, playerContext.PlayerName);
        crewRepositoriesContext.ValidationRepositoryMocks = new CrewValidationRepositoryMock(true, playerIsOwner: true);
        crewRepositoriesContext.QueryRepositoryMock = new CrewQueryRepositoryMock(new[] {crew});
    }

    [Given(@"I am a member of the crew with id ""(.*)""")]
    public void GivenIAmAMemberOfTheCrewWithId(string crewId)
    {
        var crew = crewContext.ToCrew(crewId, playerContext.PlayerName);
        crewRepositoriesContext.ValidationRepositoryMocks = new CrewValidationRepositoryMock(true);
        crewRepositoriesContext.QueryRepositoryMock = new CrewQueryRepositoryMock(new[] {crew});
    }

    [Given(@"the crew have a member with id ""(.*)""")]
    public void GivenTheCrewHaveAMemberWithId(string memberId)
    {
        crewRepositoriesContext.QueryRepositoryMock.StoredCrews[0].Members.Add(Player.Create(memberId, "memberName",2, 16));
    }
}