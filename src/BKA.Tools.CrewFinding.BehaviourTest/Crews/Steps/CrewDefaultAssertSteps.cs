using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class CrewDefaultAssertSteps(CrewRepositoriesContext crewRepositoriesContext)
{
    [Then(@"the Crew is successfully created with the default location information")]
    public void ThenTheCrewIsSuccessfullyCreatedWithTheDefaultLocationInformation()
    {
        var crewParty = crewRepositoriesContext.CommandRepositoryMock.GetStoredCrew()!;
        CrewDefaultAssert.AssertDefaultLocation(crewParty);
    }

    [Then(@"the Crew is successfully created with the default languages")]
    public void ThenTheCrewIsSuccessfullyCreatedWithTheDefaultLanguages()
    {
        var crewParty = crewRepositoriesContext.CommandRepositoryMock.GetStoredCrew()!;
        CrewDefaultAssert.AssertDefaultLanguages(crewParty);
    }

    [Then(@"the creation of the Crew is created with the default activity")]
    public void ThenTheCreationOfTheCrewIsCreatedWithTheDefaultActivity()
    {
        var crewParty = crewRepositoriesContext.CommandRepositoryMock.GetStoredCrew()!;
        CrewDefaultAssert.AssertDefaultActivity(crewParty);
    }
}