using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps.Assertions;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

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
        var crewParty = _crewRepositoriesContext.CommandRepositoryMock.GetStoredCrew()!;
        CrewDefaultAssert.AssertDefaultLocation(crewParty);
    }

    [Then(@"the Crew is successfully created with the default languages")]
    public void ThenTheCrewIsSuccessfullyCreatedWithTheDefaultLanguages()
    {
        var crewParty = _crewRepositoriesContext.CommandRepositoryMock.GetStoredCrew()!;
        CrewDefaultAssert.AssertDefaultLanguages(crewParty);
    }

    [Then(@"the creation of the Crew is created with the default activity")]
    public void ThenTheCreationOfTheCrewIsCreatedWithTheDefaultActivity()
    {
        var crewParty = _crewRepositoriesContext.CommandRepositoryMock.GetStoredCrew()!;
        CrewDefaultAssert.AssertDefaultActivity(crewParty);
    }
}