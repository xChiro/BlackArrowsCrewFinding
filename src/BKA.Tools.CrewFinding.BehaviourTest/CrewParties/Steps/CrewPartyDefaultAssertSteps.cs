using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class CrewPartyDefaultAssertSteps
{
    private readonly MockRepositoriesContext _mockRepositoriesContext;
    private readonly CrewPartyDefaultAssert _crewPartyAssert;

    public CrewPartyDefaultAssertSteps(MockRepositoriesContext mockRepositoriesContext,
        CrewPartyDefaultAssert crewPartyAssert)
    {
        _mockRepositoriesContext = mockRepositoriesContext;
        _crewPartyAssert = crewPartyAssert;
    }

    [Then(@"the Crew Party is successfully created with the default location information")]
    public void ThenTheCrewPartyIsSuccessfullyCreatedWithTheDefaultLocationInformation()
    {
        var crewParty = _mockRepositoriesContext.CrewPartyCommandsMock.GetCrewParty()!;
        _crewPartyAssert.AssertDefaultLocation(crewParty);
    }

    [Then(@"the Crew Party is successfully created with the default languages")]
    public void ThenTheCrewPartyIsSuccessfullyCreatedWithTheDefaultLanguages()
    {
        var crewParty = _mockRepositoriesContext.CrewPartyCommandsMock.GetCrewParty()!;
        _crewPartyAssert.AssertDefaultLanguages(crewParty);
    }

    [Then(@"the creation of the Crew Party is created with the default activity")]
    public void ThenTheCreationOfTheCrewPartyIsCreatedWithTheDefaultActivity()
    {
        var crewParty = _mockRepositoriesContext.CrewPartyCommandsMock.GetCrewParty()!;
        _crewPartyAssert.AssertDefaultActivity(crewParty);
    }
}