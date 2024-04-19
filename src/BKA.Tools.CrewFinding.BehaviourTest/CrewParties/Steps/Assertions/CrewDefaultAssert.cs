using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps.Assertions;

public static class CrewDefaultAssert
{
    public static void AssertDefaultLocation(Crew crew)
    {
        crew.ReunionPoint.Should().BeEquivalentTo(Location.DefaultLocation());
    }

    public static void AssertDefaultLanguages(Crew crew)
    {
        var languageExpectation = LanguageCollections.Default().Select(language => language.LanguageCode);
        crew.Languages.Select(language => language.LanguageCode).Should()
            .BeEquivalentTo(languageExpectation);
    }

    public static void AssertDefaultActivity(Crew crew)
    {
        crew.Activity.Name.Should().Be(Activity.Default().Name);
    }
}