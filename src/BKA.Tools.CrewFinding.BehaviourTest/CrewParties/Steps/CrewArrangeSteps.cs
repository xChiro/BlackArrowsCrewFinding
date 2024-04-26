using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

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
        _crewRepositoriesContext.CrewValidationRepositoryMocks =
            new CrewValidationRepositoryMock(
                new[] {crew},
                true);
    }

    [Given(@"I am the captain of an active Crew")]
    public void GivenIAmTheCaptainOfAnActiveCrew()
    {
        var crew = _crewContext.ToCrew(_playerContext.PlayerId, _playerContext.PlayerName);
        _crewRepositoriesContext.CrewValidationRepositoryMocks =
            new CrewValidationRepositoryMock(
                new[] {crew},
                true,
                playerIsOwner: true);
    }

    [Given(@"there is an active crew created by another player")]
    public void GivenThereIsAnActiveCrewCreatedByAnotherPlayer()
    {
        var crew = _crewContext.ToCrew("anotherPlayerId", "anotherPlayerName");
        _crewRepositoriesContext.CrewValidationRepositoryMocks =
            new CrewValidationRepositoryMock(
                new[] {crew},
                true);
    }
}