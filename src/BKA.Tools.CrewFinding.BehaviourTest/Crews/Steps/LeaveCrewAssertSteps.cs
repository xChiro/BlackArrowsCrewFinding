using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Commons.Exceptions;
using BKA.Tools.CrewFinding.Crews.Exceptions;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class LeaveCrewAssertSteps
{
    private readonly PlayerContext _playerContext;
    private readonly CrewRepositoriesContext _crewRepositoriesContext;
    private readonly ExceptionResultContext _exceptionResultContext;
    private readonly CrewLeaverResponseMock _crewLeaverResponseMock;

    public LeaveCrewAssertSteps(PlayerContext playerContext, CrewRepositoriesContext crewRepositoriesContext,
        CrewContext crewContext, ExceptionResultContext exceptionResultContext, CrewLeaverResponseMock crewLeaverResponseMock)
    {
        _playerContext = playerContext;
        _crewRepositoriesContext = crewRepositoriesContext;
        _exceptionResultContext = exceptionResultContext;
        _crewLeaverResponseMock = crewLeaverResponseMock;
    }

    [Then(@"the player is removed from the Crew")]
    public void ThenThePlayerIsRemovedFromTheCrew()
    {
        _crewRepositoriesContext.CommandRepositoryMock.CrewPartyMembers.Should()
            .NotContain(player => player.Id == _playerContext.PlayerId);
        _crewLeaverResponseMock.CrewId.Should().Be(_crewRepositoriesContext.CommandRepositoryMock.GetStoredCrew()?.Id);
    }

    [Then(@"the player receives a message indicating the player is not a member of the Crew")]
    public void ThenThePlayerReceivesAMessageIndicatingThePlayerIsNotAMemberOfTheCrew()
    {
        _exceptionResultContext.Exception.Should().BeOfType<PlayerNotInCrewException>();
        _crewLeaverResponseMock.CrewId.Should().BeNullOrEmpty();
    }

    [Then(@"the player receives a message indicating the Crew does not exist")]
    public void ThenThePlayerReceivesAMessageIndicatingTheCrewDoesNotExist()
    {
        _exceptionResultContext.Exception.Should().BeOfType<CrewNotFoundException>();
        _crewLeaverResponseMock.CrewId.Should().BeNullOrEmpty();
    }
}