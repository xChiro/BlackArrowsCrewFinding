using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Mocks;
using BKA.Tools.CrewFinding.Crews;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class CrewArrangeSteps

{
    private readonly CrewContext _crewContext;
    private readonly PlayerContext _playerContext;
    private readonly PlayerRepositoryContext _playerRepositoryContext;

    public CrewArrangeSteps(CrewContext crewContext, PlayerContext playerContext,
        PlayerRepositoryContext playerRepositoryContext)
    {
        _crewContext = crewContext;
        _playerContext = playerContext;
        _playerRepositoryContext = playerRepositoryContext;
    }

    [Given(@"the default MaxCrewSize is (.*)")]
    public void GivenTheDefaultMaxCrewSizeIs(string defaultMaxCrewSize)
    {
        _crewContext.MaxPlayerAllowed = int.Parse(defaultMaxCrewSize);
    }

    [Given(@"the player already has an active Crew")]
    public void GivenThePlayerAlreadyHasAnActiveCrew()
    {
        _playerRepositoryContext.PlayerQueriesMock =
            new PlayerQueriesMock(_playerContext.PlayerId, _playerContext.UserName, true);
    }
}