using BKA.Tools.CrewFinding.CrewParties;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

public class CrewPartyDefaultAssert
{
    public void AssertDefaultLocation(CrewParty crewParty)
    {
        crewParty.ReunionPoint.Should().BeEquivalentTo(Location.DefaultLocation());
    }

    public void AssertDefaultLanguages(CrewParty crewParty)
    {
        var languageExpectation = LanguageCollections.Default().Select(language => language.LanguageCode);
        crewParty.Languages.Select(language => language.LanguageCode).Should()
            .BeEquivalentTo(languageExpectation);
    }

    public void AssertDefaultActivity(CrewParty crewParty)
    {
        crewParty.Activity.Name.Should().Be(Activity.Default().Name);
    }
}