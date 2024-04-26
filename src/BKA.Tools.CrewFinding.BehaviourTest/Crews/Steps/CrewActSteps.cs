using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Helpers;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.Crews.Commands.CreateRequests;

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
        return CreateAndStoreCrew(CrewFactory.CreateDefaultCrewPartyWithoutLocation(_playerContext.PlayerId));
    }

    [When(@"the player creates a Crew with the following details:")]
    public Task WhenThePlayerCreatesACrewWithTheFollowingDetails(Table crewPartyDetails)
    {
        _crewContext.FillData(crewPartyDetails);
        return CreateAndStoreCrew(_crewContext.ToRequest(_playerContext.PlayerId));
    }

    [When(@"the player attempts to create a Crew with missing MaxCrewSize")]
    public Task WhenThePlayerAttemptsToCreateACrewWithMissingMaxCrewSize()
    {
        return CreateAndStoreCrew(CrewFactory.CreateCrew(_playerContext.PlayerId, _crewContext.MaxPlayerAllowed));
    }

    [When(@"the player attempts to create a Crew with missing languages")]
    public Task WhenThePlayerAttemptsToCreateACrewWithMissingLanguages()
    {
        return CreateAndStoreCrew(CrewFactory.CreateCrewPartyWithMissingLanguages(_playerContext.PlayerId));
    }

    [When(@"the player attempts to create a Crew with missing activity information")]
    public Task WhenThePlayerAttemptsToCreateACrewWithMissingActivityInformation()
    {
        return CreateAndStoreCrew(CrewFactory.CreateCrew(_playerContext.PlayerId, 1));
    }

    [When(@"the player attempts to create a new Crew")]
    public async Task WhenThePlayerAttemptsToCreateANewCrew()
    {
        try
        {
            var crewCreatorRequest = CrewFactory.CreateCrew(_playerContext.PlayerId, 1);
            await CreateAndStoreCrew(crewCreatorRequest);
        }
        catch (Exception ex)
        {
            _crewCreationResultsContext.Exception = ex;
        }
    }

    private async Task CreateAndStoreCrew(CrewCreatorRequest crewCreatorRequest)
    {
        var crewPartyCreator = new CrewCreator(_crewRepositoriesContext.CrewCommandRepositoryMock,
            _crewRepositoriesContext.CrewValidationRepositoryMocks,
            _playerRepositoryContext.PlayerQueryRepositoryMock,
            10);

        await crewPartyCreator.Create(crewCreatorRequest,
            _crewCreationResultsContext.CrewCreatorResponseMock);
    }
}