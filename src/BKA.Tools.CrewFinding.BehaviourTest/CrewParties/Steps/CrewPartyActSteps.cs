using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Globals;
using BKA.Tools.CrewFinding.CrewParties.CreateRequests;

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

    [When(@"the player creates a Crew Party with the following details:")]
    public Task WhenThePlayerCreatesACrewPartyWithTheFollowingDetails(Table crewPartyDetails)
    {
        _crewPartyContext.FillData(crewPartyDetails);
        return CreateAndStoreCrewParty(_crewPartyContext.ToRequest(_playerContext.PlayerId, _playerContext.PlayerName));
    }

    [When(@"the player attempts to create a Crew Party with missing MaxCrewSize")]
    public Task WhenThePlayerAttemptsToCreateACrewPartyWithMissingMaxCrewSize()
    {
        return CreateAndStoreCrewParty(CrewPartyFactory.CreateCrewParty(_playerContext.PlayerId, _playerContext.PlayerName, 0));
    }

    [When(@"the player attempts to create a Crew Party with missing languages")]
    public Task WhenThePlayerAttemptsToCreateACrewPartyWithMissingLanguages()
    {
        return CreateAndStoreCrewParty(CrewPartyFactory.CreateCrewPartyWithMissingLanguages(_playerContext.PlayerId));
    }

    [When(@"the player attempts to create a Crew Party with missing activity information")]
    public Task WhenThePlayerAttemptsToCreateACrewPartyWithMissingActivityInformation()
    {
        return CreateAndStoreCrewParty(CrewPartyFactory.CreateCrewParty(_playerContext.PlayerId,  _playerContext.PlayerName,1));
    }

    [When(@"the player attempts to create a new Crew Party")]
    public async Task WhenThePlayerAttemptsToCreateANewCrewParty()
    {
        try
        {
            await CreateAndStoreCrewParty(CrewPartyFactory.CreateCrewParty(_playerContext.PlayerId, _playerContext.PlayerName, 1));
        }
        catch (Exception ex)
        {
            _crewPartyCreationResultsContext.Exception = ex;
        }
    }

    private async Task CreateAndStoreCrewParty(CrewPartyCreatorRequest crewPartyCreatorRequest)
    {
        var crewPartyCreator = new CrewPartyCreator(_mockRepositoriesContext.CrewPartyCommandsMock,
            _mockRepositoriesContext.CrewPartyQueriesMocks,
            _crewPartyContext.MaxPlayerAllowed);
        
        await crewPartyCreator.Create(crewPartyCreatorRequest,
            _crewPartyCreationResultsContext.CrewPartyCreatorResponseMock);
    }
}