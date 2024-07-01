using BKA.Tools.CrewFinding.BehaviourTest.Commons.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Crews.Commands.JoinRequests;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class JoinCrewActSteps(
    PlayerContext playerContext,
    PlayerRepositoryContext playerRepositoryContext,
    CrewRepositoriesContext crewRepositoriesContext)
{
    private readonly UserSessionMock _userSessionMock = new(playerContext.PlayerId);

    [When(@"the player wants to join to the Crew")]
    public async Task WhenThePlayerWantsToJoinToTheCrew()
    {
        var sut = new CrewJoiner(crewRepositoriesContext.ValidationRepositoryMocks, crewRepositoriesContext.QueryRepositoryMock,
            crewRepositoriesContext.CommandRepositoryMock, playerRepositoryContext.PlayerQueryRepositoryMock,
            _userSessionMock);

        await sut.Join(crewRepositoriesContext.QueryRepositoryMock.StoredCrews[0].Id);
    }

    [When(@"the player attempts to join the Crew")]
    public async Task WhenThePlayerAttemptsToJoinTheCrew()
    {
        var sut = new CrewJoiner(crewRepositoriesContext.ValidationRepositoryMocks, crewRepositoriesContext.QueryRepositoryMock,
            crewRepositoriesContext.CommandRepositoryMock, playerRepositoryContext.PlayerQueryRepositoryMock, _userSessionMock);

        try
        {
            await sut.Join(crewRepositoriesContext.QueryRepositoryMock.StoredCrews[0].Id);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}