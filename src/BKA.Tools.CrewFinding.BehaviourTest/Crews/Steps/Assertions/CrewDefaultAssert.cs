using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Cultures;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps.Assertions;

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