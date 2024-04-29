using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class CrewArrangeSteps

{
    private readonly CrewContext _crewContext;
    private readonly CrewRepositoriesContext _crewRepositoriesContext;
    private readonly PlayerContext _playerContext;

    public CrewArrangeSteps(CrewContext crewContext, PlayerContext playerContext,
        CrewRepositoriesContext crewRepositoriesContext)
    {
        _crewContext = crewContext;
        _playerContext = playerContext;
        _crewRepositoriesContext = crewRepositoriesContext;
    }

    [Given(@"the default MaxCrewSize is (.*)")]
    public void GivenTheDefaultMaxCrewSizeIs(string defaultMaxCrewSize)
    {
        _crewContext.MaxPlayerAllowed = int.Parse(defaultMaxCrewSize);
    }

    [Given(@"the player already has an active Crew")]
    public void GivenThePlayerAlreadyHasAnActiveCrew()
    {
        var crew = _crewContext.ToCrew(_playerContext.PlayerId, _playerContext.PlayerName);
        _crewRepositoriesContext.ValidationRepositoryMocks = new CrewValidationRepositoryMock(true);
        _crewRepositoriesContext.QueryRepositoryMock = new CrewQueryRepositoryMock(new[] {crew});
    }

    [Given(@"I am the captain of an active Crew")]
    public void GivenIAmTheCaptainOfAnActiveCrew()
    {
        var crew = _crewContext.ToCrew(_playerContext.PlayerId, _playerContext.PlayerName);

        _crewRepositoriesContext.ValidationRepositoryMocks =
            new CrewValidationRepositoryMock(true, playerIsOwner: true);
        _crewRepositoriesContext.QueryRepositoryMock = new CrewQueryRepositoryMock(new[] {crew});
    }

    [Given(@"there is an active crew created by another player")]
    public void GivenThereIsAnActiveCrewCreatedByAnotherPlayer()
    {
        var crew = _crewContext.ToCrew("anotherPlayerId", "anotherPlayerName");
        _crewRepositoriesContext.ValidationRepositoryMocks = new CrewValidationRepositoryMock(true);
        _crewRepositoriesContext.QueryRepositoryMock = new CrewQueryRepositoryMock(new[] {crew});
    }

    [Given(@"the player is the captain of a Crew")]
    public void GivenThePlayerIsTheCaptainOfACrew()
    {
        var crew = _crewContext.ToCrew(_playerContext.PlayerId, _playerContext.PlayerName);
        _crewRepositoriesContext.ValidationRepositoryMocks = new CrewValidationRepositoryMock(true, playerIsOwner: true);
        _crewRepositoriesContext.QueryRepositoryMock = new CrewQueryRepositoryMock(new[] {crew});
    }
}