using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps.Assertions;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class CrewDefaultAssertSteps
{
    private readonly CrewRepositoriesContext _crewRepositoriesContext;

    public CrewDefaultAssertSteps(CrewRepositoriesContext crewRepositoriesContext)
    {
        _crewRepositoriesContext = crewRepositoriesContext;
    }

    [Then(@"the Crew is successfully created with the default location information")]
    public void ThenTheCrewIsSuccessfullyCreatedWithTheDefaultLocationInformation()
    {
        var crewParty = _crewRepositoriesContext.CrewCommandRepositoryMock.GetStoredCrew()!;
        CrewDefaultAssert.AssertDefaultLocation(crewParty);
    }

    [Then(@"the Crew is successfully created with the default languages")]
    public void ThenTheCrewIsSuccessfullyCreatedWithTheDefaultLanguages()
    {
        var crewParty = _crewRepositoriesContext.CrewCommandRepositoryMock.GetStoredCrew()!;
        CrewDefaultAssert.AssertDefaultLanguages(crewParty);
    }

    [Then(@"the creation of the Crew is created with the default activity")]
    public void ThenTheCreationOfTheCrewIsCreatedWithTheDefaultActivity()
    {
        var crewParty = _crewRepositoriesContext.CrewCommandRepositoryMock.GetStoredCrew()!;
        CrewDefaultAssert.AssertDefaultActivity(crewParty);
    }
}