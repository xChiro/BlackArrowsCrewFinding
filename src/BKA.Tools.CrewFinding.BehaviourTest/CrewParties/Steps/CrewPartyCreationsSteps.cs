using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Globals;
using BKA.Tools.CrewFinding.CrewParties;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class CrewPartyCreationsSteps
{
    private readonly CrewPartyContext _crewPartyContext;
    private readonly PlayerContext _playerContext;
    private readonly CreatePartyResultsContext _createPartyResultsContext;

    public CrewPartyCreationsSteps(CrewPartyContext crewPartyContext, PlayerContext playerContext,
        CreatePartyResultsContext createPartyResultsContext)
    {
        _crewPartyContext = crewPartyContext;
        _playerContext = playerContext;
        _createPartyResultsContext = createPartyResultsContext;
    }

    [When(@"the player creates a Crew Party named '(.*)' with the following details:")]
    public void When_whenThePlayerCreatesACrewPartyNamedWithTheFollowingDetails(string crewPartyName,
        Table crewPartyDetails)
    {
        _crewPartyContext.FillData(crewPartyName, crewPartyDetails);

        var crewPartyCreatorRequest = _crewPartyContext.ToRequest(_playerContext.UserName);
        _createPartyResultsContext.CrewPartyCommandsMock = new CrewPartyCommandsMock();

        var crewPartyCreator = new CrewPartyCreator(_createPartyResultsContext.CrewPartyCommandsMock, 10);

        crewPartyCreator.Create(crewPartyCreatorRequest);
    }

    [When(@"the player creates a Crew Party named '(.*)' with missing location information")]
    public void When_whenThePlayerCreatesACrewPartyNamedWithMissingLocationInformation(string p0)
    {
        _createPartyResultsContext.CrewPartyCommandsMock = new CrewPartyCommandsMock();
        var crewPartyCreator = new CrewPartyCreator(_createPartyResultsContext.CrewPartyCommandsMock, 10);

        var crewPartyCreatorRequest = CrewPartyFactory.CreateDefaultCrewPartyWithOutLocation(_playerContext.UserName);
        crewPartyCreator.Create(crewPartyCreatorRequest);
    }
}