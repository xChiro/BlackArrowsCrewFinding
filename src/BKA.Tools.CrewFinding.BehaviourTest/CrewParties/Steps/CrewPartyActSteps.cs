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
    public Task WhenThePlayerAttemptsToCreateACrewPartyWithMissingLocationInformation()
    {
        return CreateAndStoreCrewParty(CrewPartyFactory.CreateDefaultCrewPartyWithoutLocation(_playerContext.PlayerId));
    }

    [When(@"the player creates a Crew Party named '(.*)' with the following details:")]
    public Task WhenThePlayerCreatesACrewPartyNamedWithTheFollowingDetails(string crewPartyName, Table crewPartyDetails)
    {
        _crewPartyContext.FillData(crewPartyName, crewPartyDetails);
        return CreateAndStoreCrewParty(_crewPartyContext.ToRequest(_playerContext.PlayerId));
    }

    [When(@"the player attempts to create a Crew Party with missing MaxCrewSize")]
    public Task WhenThePlayerAttemptsToCreateACrewPartyWithMissingMaxCrewSize()
    {
        return CreateAndStoreCrewParty(CrewPartyFactory.CreateCrewParty(_playerContext.PlayerId, 0));
    }

    [When(@"the player attempts to create a Crew Party with missing languages")]
    public Task WhenThePlayerAttemptsToCreateACrewPartyWithMissingLanguages()
    {
        return CreateAndStoreCrewParty(CrewPartyFactory.CreateCrewPartyWithMissingLanguages(_playerContext.PlayerId));
    }

    [When(@"the player attempts to create a Crew Party with missing activity information")]
    public Task WhenThePlayerAttemptsToCreateACrewPartyWithMissingActivityInformation()
    {
        return CreateAndStoreCrewParty(CrewPartyFactory.CreateCrewParty(_playerContext.PlayerId, 1));
    }

    [When(@"the player attempts to create a new Crew Party")]
    public async Task WhenThePlayerAttemptsToCreateANewCrewParty()
    {
        try
        {
            await CreateAndStoreCrewParty(CrewPartyFactory.CreateCrewParty(_playerContext.PlayerId, 1));
        }
        catch (Exception ex)
        {
            _crewPartyCreationResultsContext.Exception = ex;
        }
    }

    private async Task CreateAndStoreCrewParty(CrewPartyCreatorRequest crewPartyCreatorRequest)
    {
        _mockRepositoriesContext.PlayerQueriesMock =
            new PlayerQueriesMock(_playerContext.PlayerId, _playerContext.UserName);
        
        var crewPartyCreator = new CrewPartyCreator(_mockRepositoriesContext.CrewPartyCommandsMock,
            _mockRepositoriesContext.CrewPartyQueriesMocks,
            _crewPartyContext.MaxPlayerAllowed,
            _mockRepositoriesContext.PlayerQueriesMock);
        
        await crewPartyCreator.Create(crewPartyCreatorRequest,
            _crewPartyCreationResultsContext.CrewPartyCreatorResponseMock);
    }
}