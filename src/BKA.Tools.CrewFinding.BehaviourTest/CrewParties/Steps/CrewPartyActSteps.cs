using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Helpers;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Context;
using BKA.Tools.CrewFinding.BehaviourTest.Players.Mocks;
using BKA.Tools.CrewFinding.CrewParties.CreateRequests;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class CrewPartyActSteps
{
    private readonly CrewPartyContext _crewPartyContext;
    private readonly PlayerContext _playerContext;
    private readonly PlayerRepositoryContext _playerRepositoryContext;
    private readonly CrewPartyRepositoriesContext _crewPartyRepositoriesContext;
    private readonly CrewPartyCreationResultsContext _crewPartyCreationResultsContext;

    public CrewPartyActSteps(CrewPartyContext crewPartyContext, 
        PlayerContext playerContext,
        PlayerRepositoryContext playerRepositoryContext,
        CrewPartyRepositoriesContext crewPartyRepositoriesContext,
        CrewPartyCreationResultsContext crewPartyCreationResultsContext)
    {
        _crewPartyContext = crewPartyContext;
        _playerContext = playerContext;
        _playerRepositoryContext = playerRepositoryContext;
        _crewPartyRepositoriesContext = crewPartyRepositoriesContext;
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
        _playerRepositoryContext.PlayerQueriesMock =
            new PlayerQueriesMock(_playerContext.PlayerId, _playerContext.UserName);

        var crewPartyCreator = new CrewPartyCreator(_crewPartyRepositoriesContext.CrewPartyCommandsMock,
            _crewPartyRepositoriesContext.CrewPartyQueriesMocks,
            _crewPartyContext.MaxPlayerAllowed,
            _playerRepositoryContext.PlayerQueriesMock);

        await crewPartyCreator.Create(crewPartyCreatorRequest,
            _crewPartyCreationResultsContext.CrewPartyCreatorResponseMock);
    }
}