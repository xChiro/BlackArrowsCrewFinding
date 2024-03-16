using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class CrewPartyAssertSteps
{
    private readonly CreatePartyResultsContext _createPartyResultsContext;

    public CrewPartyAssertSteps(CreatePartyResultsContext createPartyResultsContext)
    {
        _createPartyResultsContext = createPartyResultsContext;
    }

    [Then(@"a Crew Party with default name is successfully created for the player (.*)")]
    public void TCrewPartyNamed_isSuccessfullyCreated(string playerName)
    {
        _createPartyResultsContext.CrewPartyCommandsMock.GetCrewParty()?.Name.Value.Should()
            .Be($"Crew Party of {playerName}");
    }

    [Then(@"the Crew Party contains the following details:")]
    public void ThenTheCrewPartyContainsTheFollowingDetails(Table table)
    {
        var crewParty = _createPartyResultsContext.CrewPartyCommandsMock.GetCrewParty();
        crewParty.Should().NotBeNull();
        crewParty!.Activity.Description.Should().Be(table.Rows[0]["Description"]);
        crewParty.Activity.Name.Should().Be(table.Rows[0]["Activity"]);
        crewParty.ReunionPoint.Place.Should().Be(table.Rows[0]["Place"]);
        crewParty.ReunionPoint.PlanetMoon.Should().Be(table.Rows[0]["Planet/Moon"]);
        crewParty.ReunionPoint.PlanetarySystem.Should().Be(table.Rows[0]["PlanetarySystem"]);
        crewParty.ReunionPoint.System.Should().Be(table.Rows[0]["System"]);
        crewParty.TotalCrewNumber.Value.Should().Be(int.Parse(table.Rows[0]["CrewSize"]));

        var expectation = table.Rows[0]["Languages"].Split(',').Select(x => x.Trim()).ToList();
        crewParty.Languages.Select(language => language.LanguageCode).Should().BeEquivalentTo(expectation);
    }

    [Then(@"the creation date is the current date")]
    public void ThenTheCreationDateIsTheCurrentDate()
    {
        _createPartyResultsContext.CrewPartyCommandsMock.GetCrewParty()?.CreationDate.Should()
            .BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Then(@"(.*) is designated as the Captain")]
    public void ThenIsDesignatedAsTheCaptain(string userName)
    {
        _createPartyResultsContext.CrewPartyCommandsMock.GetCaptain()!.Name.Should().Be(userName);
    }

    [Then(@"the Crew Party is successfully created with the default location information")]
    public void ThenTheCrewPartyIsSuccessfullyCreatedWithTheDefaultLocationInformation()
    {
        var crewParty = _createPartyResultsContext.CrewPartyCommandsMock.GetCrewParty();
        crewParty!.ReunionPoint.Should().BeEquivalentTo(Location.DefaultLocation());
    }

    [Then(@"the Crew Party of (.*) is successfully created")]
    public void ThenTheCrewPartyNamedSCrewIsSuccessfullyCreated(string captainName)
    {
        _createPartyResultsContext.CrewPartyCommandsMock.GetCrewParty().Should().NotBeNull();
        _createPartyResultsContext.CrewPartyCommandsMock.GetCrewParty()!.Name.Value.Should()
            .Be($"Crew Party of {captainName}");
    }

    [Then(@"the MaxCrewSize is set to (.*)")]
    public void ThenTheMaxCrewSizeIsSetTo(string defaultMaxCrewSize)
    {
        _createPartyResultsContext.CrewPartyCommandsMock.GetCrewParty()!.TotalCrewNumber.Value.Should()
            .Be(int.Parse(defaultMaxCrewSize));
    }

    [Then(@"the Crew Party is successfully created with the default languages")]
    public void ThenTheCrewPartyIsSuccessfullyCreatedWithTheDefaultLanguages()
    {
        var crewParty = _createPartyResultsContext.CrewPartyCommandsMock.GetCrewParty();
        var languageExpectation = LanguageCollections.Default().Select(language => language.LanguageCode);
        crewParty!.Languages.Select(language => language.LanguageCode).Should()
            .BeEquivalentTo(languageExpectation);
    }

    [Then(@"the creation of the Crew Party is created with the default activity")]
    public void ThenTheCreationOfTheCrewPartyIsCreatedWithTheDefaultActivity()
    {
        _createPartyResultsContext.CrewPartyCommandsMock.GetCrewParty()!.Activity.Name.Should()
            .Be(Activity.Default().Name);
    }
}