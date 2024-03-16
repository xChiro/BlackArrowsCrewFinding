using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class CrewPartyDefaultAssertSteps
{
    private readonly MockRepositoriesContext _mockRepositoriesContext;

    public CrewPartyDefaultAssertSteps(MockRepositoriesContext mockRepositoriesContext)
    {
        _mockRepositoriesContext = mockRepositoriesContext;
    }

    [Then(@"the Crew Party is successfully created with the default location information")]
    public void ThenTheCrewPartyIsSuccessfullyCreatedWithTheDefaultLocationInformation()
    {
        var crewParty = _mockRepositoriesContext.CrewPartyCommandsMock.GetCrewParty();
        crewParty!.ReunionPoint.Should().BeEquivalentTo(Location.DefaultLocation());
    }

    [Then(@"the Crew Party is successfully created with the default languages")]
    public void ThenTheCrewPartyIsSuccessfullyCreatedWithTheDefaultLanguages()
    {
        var crewParty = _mockRepositoriesContext.CrewPartyCommandsMock.GetCrewParty();
        var languageExpectation = LanguageCollections.Default().Select(language => language.LanguageCode);
        crewParty!.Languages.Select(language => language.LanguageCode).Should()
            .BeEquivalentTo(languageExpectation);
    }

    [Then(@"the creation of the Crew Party is created with the default activity")]
    public void ThenTheCreationOfTheCrewPartyIsCreatedWithTheDefaultActivity()
    {
        _mockRepositoriesContext.CrewPartyCommandsMock.GetCrewParty()!.Activity.Name.Should()
            .Be(Activity.Default().Name);
    }
}