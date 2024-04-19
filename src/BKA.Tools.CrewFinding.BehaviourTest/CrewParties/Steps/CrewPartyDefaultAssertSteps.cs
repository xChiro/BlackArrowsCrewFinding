using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps.Assertions;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class CrewPartyDefaultAssertSteps
{
    private readonly CrewPartyRepositoriesContext _crewPartyRepositoriesContext;
    private readonly CrewPartyDefaultAssert _crewPartyAssert;

    public CrewPartyDefaultAssertSteps(CrewPartyRepositoriesContext crewPartyRepositoriesContext,
        CrewPartyDefaultAssert crewPartyAssert)
    {
        _crewPartyRepositoriesContext = crewPartyRepositoriesContext;
        _crewPartyAssert = crewPartyAssert;
    }

    [Then(@"the Crew Party is successfully created with the default location information")]
    public void ThenTheCrewPartyIsSuccessfullyCreatedWithTheDefaultLocationInformation()
    {
        var crewParty = _crewPartyRepositoriesContext.CrewPartyCommandsMock.GetCrewParty()!;
        _crewPartyAssert.AssertDefaultLocation(crewParty);
    }

    [Then(@"the Crew Party is successfully created with the default languages")]
    public void ThenTheCrewPartyIsSuccessfullyCreatedWithTheDefaultLanguages()
    {
        var crewParty = _crewPartyRepositoriesContext.CrewPartyCommandsMock.GetCrewParty()!;
        _crewPartyAssert.AssertDefaultLanguages(crewParty);
    }

    [Then(@"the creation of the Crew Party is created with the default activity")]
    public void ThenTheCreationOfTheCrewPartyIsCreatedWithTheDefaultActivity()
    {
        var crewParty = _crewPartyRepositoriesContext.CrewPartyCommandsMock.GetCrewParty()!;
        _crewPartyAssert.AssertDefaultActivity(crewParty);
    }
}