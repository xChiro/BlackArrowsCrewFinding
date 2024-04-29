using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Crews.Exceptions;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class LeaveCrewAssertSteps
{
    private readonly PlayerContext _playerContext;
    private readonly CrewRepositoriesContext _crewRepositoriesContext;
    private readonly ExceptionResultContext _exceptionResultContext;

    public LeaveCrewAssertSteps(PlayerContext playerContext, CrewRepositoriesContext crewRepositoriesContext,
        CrewContext crewContext, ExceptionResultContext exceptionResultContext)
    {
        _playerContext = playerContext;
        _crewRepositoriesContext = crewRepositoriesContext;
        _exceptionResultContext = exceptionResultContext;
    }

    [Then(@"the player is removed from the Crew")]
    public void ThenThePlayerIsRemovedFromTheCrew()
    {
        _crewRepositoriesContext.CommandRepositoryMock.CrewPartyMembers.Should()
            .NotContain(player => player.Id == _playerContext.PlayerId);
    }

    [Then(@"the player receives a message indicating the player is not a member of the Crew")]
    public void ThenThePlayerReceivesAMessageIndicatingThePlayerIsNotAMemberOfTheCrew()
    {
        _exceptionResultContext.Exception.Should().BeOfType<PlayerNotInCrewException>();
    }

    [Then(@"the player receives a message indicating the Crew does not exist")]
    public void ThenThePlayerReceivesAMessageIndicatingTheCrewDoesNotExist()
    {
        _exceptionResultContext.Exception.Should().BeOfType<CrewNotFoundException>();
    }
}