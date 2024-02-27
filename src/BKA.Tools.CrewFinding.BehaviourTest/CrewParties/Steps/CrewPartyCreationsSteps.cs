using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class CrewPartyCreationsSteps
{
    private readonly CrewPartyContext _crewPartyContext;

    public CrewPartyCreationsSteps(CrewPartyContext crewPartyContext)
    {
        _crewPartyContext = crewPartyContext;
    }

    [When(@"the player creates a Crew Party named '(.*)' with the following details:")]
    public void When_whenThePlayerCreatesACrewPartyNamedWithTheFollowingDetails(string crewPartyName,
        Table crewPartyDetails)
    {
        _crewPartyContext.FillData(crewPartyName, crewPartyDetails);
    }

    [Then(@"the Crew Party named (.*)'s Crew is successfully created with the specified details")]
    public void When_thenTheCrewPartyNamedSCrewIsSuccessfullyCreatedWithTheSpecifiedDetails(string userName)
    {
        ScenarioContext.StepIsPending();
    }
}