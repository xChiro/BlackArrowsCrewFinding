using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Globals;
using BKA.Tools.CrewFinding.CrewParties.Creators;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class CrewPartyActSteps
{
    private readonly CrewPartyContext _crewPartyContext;
    private readonly PlayerContext _playerContext;
    private readonly MockRepositoriesContext _mockRepositoriesContext;
    private readonly CrewPartyCreationResultsContext _crewPartyCreationResultsContext;

    public CrewPartyActSteps(CrewPartyContext crewPartyContext, PlayerContext playerContext,
        MockRepositoriesContext mockRepositoriesContext,
        CrewPartyCreationResultsContext crewPartyCreationResultsContext)
    {
        _crewPartyContext = crewPartyContext;
        _playerContext = playerContext;
        _mockRepositoriesContext = mockRepositoriesContext;
        _crewPartyCreationResultsContext = crewPartyCreationResultsContext;
    }

    [When(@"the player attempts to create a Crew Party with missing location information")]
    public async void WhenThePlayerAttemptsToCreateACrewPartyWithMissingLocationInformation()
    {
        var crewPartyCreatorRequest = CrewPartyFactory.CreateDefaultCrewPartyWithoutLocation(_playerContext.UserName);
        await CreateCrewParty(crewPartyCreatorRequest);
    }

    [When(@"the player creates a Crew Party named '(.*)' with the following details:")]
    public async void WhenThePlayerCreatesACrewPartyNamedWithTheFollowingDetails(string crewPartyName,
        Table crewPartyDetails)
    {
        _crewPartyContext.FillData(crewPartyName, crewPartyDetails);

        var crewPartyCreatorRequest = _crewPartyContext.ToRequest(_playerContext.UserName);
        var crewPartyCreator = InitializeCrewPartyCreator();

        await crewPartyCreator.Create(crewPartyCreatorRequest,
            _crewPartyCreationResultsContext.CrewPartyCreatorResponseMock);
    }

    [When(@"the player attempts to create a Crew Party with missing MaxCrewSize")]
    public async void WhenThePlayerAttemptsToCreateACrewPartyWithMissingMaxCrewSize()
    {
        var crewPartyCreatorRequest = CrewPartyFactory.CreateCrewParty(_playerContext.UserName, 0);
        await CreateCrewParty(crewPartyCreatorRequest);
    }

    private async Task CreateCrewParty(CrewPartyCreatorRequest crewPartyCreatorRequest)
    {
        var crewPartyCreator = InitializeCrewPartyCreator();

        await crewPartyCreator.Create(crewPartyCreatorRequest,
            _crewPartyCreationResultsContext.CrewPartyCreatorResponseMock);
    }

    [When(@"the player attempts to create a Crew Party with missing languages")]
    public async void WhenThePlayerAttemptsToCreateACrewPartyWithMissingLanguages()
    {
        var crewPartyCreatorRequest = CrewPartyFactory.CreateCrewPartyWithMissingLanguages(_playerContext.UserName);
        await CreateCrewParty(crewPartyCreatorRequest);
    }

    [When(@"the player attempts to create a Crew Party with missing activity information")]
    public async void WhenThePlayerAttemptsToCreateACrewPartyWithMissingActivityInformation()
    {
        var crewPartyCreatorRequest = CrewPartyFactory.CreateCrewParty(_playerContext.UserName, 1);
        
        await CreateCrewParty(crewPartyCreatorRequest);
    }

    [When(@"the player attempts to create a new Crew Party")]
    public async Task WhenThePlayerAttemptsToCreateANewCrewParty()
    {
        var crewPartyCreatorRequest = CrewPartyFactory.CreateCrewParty(_playerContext.UserName, 1);
        
        try
        {
            await CreateCrewParty(crewPartyCreatorRequest);
        }
        catch (Exception ex)
        {
            _crewPartyCreationResultsContext.Exception = ex;
        }
    }

    private CrewPartyCreator InitializeCrewPartyCreator()
    {
        _mockRepositoriesContext.CrewPartyCommandsMock = new CrewPartyCommandsMock();

        return new CrewPartyCreator(_mockRepositoriesContext.CrewPartyCommandsMock,
            _mockRepositoriesContext.CrewPartyQueriesMocks,
            _crewPartyContext.MaxPlayerAllowed,
            _mockRepositoriesContext.PlayerQueriesMock);
    }
}