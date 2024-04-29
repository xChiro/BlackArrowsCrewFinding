using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Leave;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews;

[Binding]
public class LeaveCrewActSteps
{
    private readonly PlayerContext _playerContext;
    private readonly CrewContext _crewContext;
    private readonly CrewRepositoriesContext _crewRepositoriesContext;
    private readonly ExceptionResultContext _exceptionResultContext;

    public LeaveCrewActSteps(PlayerContext playerContext, CrewContext crewContext,
        CrewRepositoriesContext crewRepositoriesContext, ExceptionResultContext exceptionResultContext)
    {
        _playerContext = playerContext;
        _crewContext = crewContext;
        _crewRepositoriesContext = crewRepositoriesContext;
        _exceptionResultContext = exceptionResultContext;
    }

    private CrewLeaver CreateCrewLeaver()
    {
        return new CrewLeaver(_crewRepositoriesContext.QueryRepositoryMock,
            _crewRepositoriesContext.CommandRepositoryMock);
    }

    [When("the player requests to leave the Crew")]
    public async Task WhenThePlayerRequestsToLeaveTheCrew()
    {
        var sut = CreateCrewLeaver();
        await sut.Leave(_playerContext.PlayerId, _crewContext.CrewId);
    }

    [When(@"the player attempts to leave the Crew")]
    public async Task WhenThePlayerAttemptsToLeaveTheCrew()
    {
        var sut = CreateCrewLeaver();
        try
        {
            await sut.Leave(_playerContext.PlayerId, _crewContext.CrewId);
        }
        catch (Exception e)
        {
            _exceptionResultContext.Exception = e;
        }
    }

    [When(@"the captain attempts to leave the Crew")]
    public async Task WhenTheCaptainAttemptsToLeaveTheCrew()
    {
        var sut = CreateCrewLeaver();
        try
        {
            await sut.Leave(_playerContext.PlayerId, _crewContext.CrewId);
        }
        catch (Exception e)
        {
            _exceptionResultContext.Exception = e;
        }
    }

    [When(@"the player attempts to leave from non-existent Crew")]
    public async Task WhenThePlayerAttemptsToLeaveFromNonExistentCrew()
    {
        var sut = CreateCrewLeaver();
        try
        {
            await sut.Leave(_playerContext.PlayerId, Guid.NewGuid().ToString());
        }
        catch (Exception e)
        {
            _exceptionResultContext.Exception = e;
        }
    }
}