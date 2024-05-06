using BKA.Tools.CrewFinding.BehaviourTest.Commons.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Helpers;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class CrewActSteps
{
    private readonly CrewContext _crewContext;
    private readonly PlayerContext _playerContext;
    private readonly PlayerRepositoryContext _playerRepositoryContext;
    private readonly CrewRepositoriesContext _crewRepositoriesContext;
    private readonly CrewCreationResultsContext _crewCreationResultsContext;

    public CrewActSteps(CrewContext crewContext, 
        PlayerContext playerContext,
        PlayerRepositoryContext playerRepositoryContext,
        CrewRepositoriesContext crewRepositoriesContext,
        CrewCreationResultsContext crewCreationResultsContext)
    {
        _crewContext = crewContext;
        _playerContext = playerContext;
        _playerRepositoryContext = playerRepositoryContext;
        _crewRepositoriesContext = crewRepositoriesContext;
        _crewCreationResultsContext = crewCreationResultsContext;
    }

    [When(@"the player attempts to create a Crew with missing location information")]
    public Task WhenThePlayerAttemptsToCreateACrewWithMissingLocationInformation()
    {
        return CreateAndStoreCrew(CrewCreatorRequestFactory.CreateDefaultCrewPartyWithoutLocation());
    }

    [When(@"the player creates a Crew with the following details:")]
    public Task WhenThePlayerCreatesACrewWithTheFollowingDetails(Table crewPartyDetails)
    {
        _crewContext.FillData(crewPartyDetails);
        return CreateAndStoreCrew(_crewContext.ToRequest());
    }

    [When(@"the player attempts to create a Crew with missing MaxCrewSize")]
    public Task WhenThePlayerAttemptsToCreateACrewWithMissingMaxCrewSize()
    {
        return CreateAndStoreCrew(CrewCreatorRequestFactory.CreateCrew(_crewContext.MaxPlayerAllowed));
    }

    [When(@"the player attempts to create a Crew with missing languages")]
    public Task WhenThePlayerAttemptsToCreateACrewWithMissingLanguages()
    {
        return CreateAndStoreCrew(CrewCreatorRequestFactory.CreateCrewPartyWithMissingLanguages());
    }

    [When(@"the player attempts to create a Crew with missing activity information")]
    public Task WhenThePlayerAttemptsToCreateACrewWithMissingActivityInformation()
    {
        return CreateAndStoreCrew(CrewCreatorRequestFactory.CreateCrew(1));
    }

    [When(@"the player attempts to create a new Crew")]
    public async Task WhenThePlayerAttemptsToCreateANewCrew()
    {
        try
        {
            var crewCreatorRequest = CrewCreatorRequestFactory.CreateCrew(1);
            await CreateAndStoreCrew(crewCreatorRequest);
        }
        catch (Exception ex)
        {
            _crewCreationResultsContext.Exception = ex;
        }
    }

    private async Task CreateAndStoreCrew(CrewCreatorRequest crewCreatorRequest)
    {
        var crewPartyCreator = new CrewCreator(_crewRepositoriesContext.CommandRepositoryMock,
            _crewRepositoriesContext.ValidationRepositoryMocks,
            _playerRepositoryContext.PlayerQueryRepositoryMock,
            new UserSessionMock(_playerContext.PlayerId), 10);

        await crewPartyCreator.Create(crewCreatorRequest,
            _crewCreationResultsContext.CrewCreatorResponseMock);
    }
}