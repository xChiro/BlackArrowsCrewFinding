using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Globals;
using BKA.Tools.CrewFinding.CrewParties;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class CrewPartyActSteps
{
    private readonly CrewPartyContext _crewPartyContext;
    private readonly PlayerContext _playerContext;
    private readonly CreatePartyResultsContext _createPartyResultsContext;

    public CrewPartyActSteps(CrewPartyContext crewPartyContext, PlayerContext playerContext,
        CreatePartyResultsContext createPartyResultsContext)
    {
        _crewPartyContext = crewPartyContext;
        _playerContext = playerContext;
        _createPartyResultsContext = createPartyResultsContext;
    }

    [When(@"the player attempts to create a Crew Party with missing location information")]
    public void WhenThePlayerCreatesACrewPartyNamedWithMissingLocationInformation()
    {
        var crewPartyCreator = InitializeCrewPartyCreator();
        var crewPartyCreatorRequest = CrewPartyFactory.CreateDefaultCrewPartyWithoutLocation(_playerContext.UserName);

        crewPartyCreator.Create(crewPartyCreatorRequest);
    }

    [When(@"the player creates a Crew Party named '(.*)' with the following details:")]
    public void WhenThePlayerCreatesACrewPartyNamedWithTheFollowingDetails(string crewPartyName,
        Table crewPartyDetails)
    {
        _crewPartyContext.FillData(crewPartyName, crewPartyDetails);

        var crewPartyCreatorRequest = _crewPartyContext.ToRequest(_playerContext.UserName);
        var crewPartyCreator = InitializeCrewPartyCreator();

        crewPartyCreator.Create(crewPartyCreatorRequest);
    }

    [When(@"the player attempts to create a Crew Party with missing MaxCrewSize")]
    public void WhenThePlayerAttemptsToCreateACrewPartyWithMissingMaxCrewSize()
    {
        var crewPartyCreator = InitializeCrewPartyCreator(_crewPartyContext.MaxPlayerAllowed);
        var crewPartyCreatorRequest = CrewPartyFactory.CreateCrewParty(_playerContext.UserName, 0);

        crewPartyCreator.Create(crewPartyCreatorRequest);
    }

    [When(@"the player attempts to create a Crew Party with missing languages")]
    public void WhenThePlayerAttemptsToCreateACrewPartyWithMissingLanguages()
    {
        var crewPartyCreator = InitializeCrewPartyCreator();
        var crewPartyCreatorRequest = CrewPartyFactory.CreateCrewPartyWithMissingLanguages(_playerContext.UserName);

        crewPartyCreator.Create(crewPartyCreatorRequest);
    }

    [When(@"the player attempts to create a Crew Party with missing activity information")]
    public void WhenThePlayerAttemptsToCreateACrewPartyWithMissingActivityInformation()
    {
        var crewPartyCreator = InitializeCrewPartyCreator();
        var crewPartyCreatorRequest = new CrewPartyCreatorRequest(_playerContext.UserName, 1,
            new Location("Stanton", "Hurston", "Arial", "Ground Station"), new[] {"en"}, "",
            "Hunt down the most dangerous criminals in the galaxy.");

        crewPartyCreator.Create(crewPartyCreatorRequest);
    }

    private CrewPartyCreator InitializeCrewPartyCreator(int maxCrewAllowed = 10)
    {
        _createPartyResultsContext.CrewPartyCommandsMock = new CrewPartyCommandsMock();

        return new CrewPartyCreator(_createPartyResultsContext.CrewPartyCommandsMock, maxCrewAllowed);
    }
}