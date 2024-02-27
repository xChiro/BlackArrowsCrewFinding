using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class CrewPartyAssertionsSteps
{
    private readonly CreatePartyResultsContext _createPartyResultsContext;

    public CrewPartyAssertionsSteps(CreatePartyResultsContext createPartyResultsContext)
    {
        _createPartyResultsContext = createPartyResultsContext;
    }

    [Then(@"a Crew Party named (.*) is successfully created")]
    public void Then_ACrewPartyNamed_isSuccessfullyCreated(string crewPartyDefaultName)
    {
        _createPartyResultsContext.CrewPartyCommandsMock.GetCrewParty()?.Name.Value.Should().Be(crewPartyDefaultName);
    }

    [Then(@"the Crew Party contains the following details:")]
    public void When_thenTheCrewPartyContainsTheFollowingDetails(Table table)
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
    public void When_thenTheCreationDateIsTheCurrentDate()
    {
        _createPartyResultsContext.CrewPartyCommandsMock.GetCrewParty()?.CreationDate.Should()
            .BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Then(@"(.*) is designated as the Captain")]
    public void Then_IsDesignatedAsTheCaptain(string userName)
    {
        _createPartyResultsContext.CrewPartyCommandsMock.GetCaptain()!.Name.Should().Be(userName);
    }
}