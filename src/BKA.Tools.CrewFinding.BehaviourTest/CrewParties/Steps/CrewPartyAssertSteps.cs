using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.CrewParties.Exceptions;
using BKA.Tools.CrewFinding.Cultures.Exceptions;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class CrewPartyAssertSteps
{
    private readonly MockRepositoriesContext _mockRepositoriesContext;
    private readonly CrewPartyCreationResultsContext _crewPartyCreationResultsContext;

    public CrewPartyAssertSteps(MockRepositoriesContext mockRepositoriesContext, 
        CrewPartyCreationResultsContext crewPartyCreationResultsContext)
    {
        _mockRepositoriesContext = mockRepositoriesContext;
        _crewPartyCreationResultsContext = crewPartyCreationResultsContext;
    }

    [Then(@"a Crew Party with default name is successfully created for the player (.*)")]
    public void ThenACrewPartyWithDefaultNameIsSuccessfullyCreatedForThePlayer(string playerName)
    {
        _mockRepositoriesContext.CrewPartyCommandsMock.GetCrewParty()?.Name.Value.Should()
            .Be($"Crew Party of {playerName}");
    }

    [Then(@"the Crew Party contains the following details:")]
    public void ThenTheCrewPartyContainsTheFollowingDetails(Table table)
    {
        var crewParty = _mockRepositoriesContext.CrewPartyCommandsMock.GetCrewParty();
        crewParty.Should().NotBeNull();
        crewParty!.Activity.Description.Should().Be(table.Rows[0]["Description"]);
        crewParty.Activity.Name.Should().Be(table.Rows[0]["Activity"]);
        crewParty.ReunionPoint.Place.Should().Be(table.Rows[0]["Place"]);
        crewParty.ReunionPoint.PlanetMoon.Should().Be(table.Rows[0]["Planet/Moon"]);
        crewParty.ReunionPoint.PlanetarySystem.Should().Be(table.Rows[0]["PlanetarySystem"]);
        crewParty.ReunionPoint.System.Should().Be(table.Rows[0]["System"]);
        crewParty.TotalCrewNumber.Current.Should().Be(int.Parse(table.Rows[0]["CrewSize"]));
        
        var expectation = table.Rows[0]["Languages"].Split(',').Select(x => x.Trim()).ToList();
        crewParty.Languages.Select(language => language.LanguageCode).Should().BeEquivalentTo(expectation);

        _crewPartyCreationResultsContext.CrewPartyCreatorResponseMock.Id.Should().NotBeNullOrEmpty();
    }

    [Then(@"the creation date is the current date")]
    public void ThenTheCreationDateIsTheCurrentDate()
    {
        _mockRepositoriesContext.CrewPartyCommandsMock.GetCrewParty()?.CreationDate.Should()
            .BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Then(@"(.*) is designated as the Captain")]
    public void ThenIsDesignatedAsTheCaptain(string userName)
    {
        _mockRepositoriesContext.CrewPartyCommandsMock.GetCaptain()!.Name.Should().Be(userName);
    }

    [Then(@"the Crew Party of (.*) is successfully created")]
    public void ThenTheCrewPartyNamedSCrewIsSuccessfullyCreated(string captainName)
    {
        _mockRepositoriesContext.CrewPartyCommandsMock.GetCrewParty().Should().NotBeNull();
        _mockRepositoriesContext.CrewPartyCommandsMock.GetCrewParty()!.Name.Value.Should()
            .Be($"Crew Party of {captainName}");
    }

    [Then(@"the MaxCrewSize is set to (.*)")]
    public void ThenTheMaxCrewSizeIsSetTo(string defaultMaxCrewSize)
    {
        _mockRepositoriesContext.CrewPartyCommandsMock.GetCrewParty()!.TotalCrewNumber.Current.Should()
            .Be(int.Parse(defaultMaxCrewSize));
    }

    [Then(@"the creation of the new Crew Party is prevented")]
    public void ThenTheCreationOfTheNewCrewPartyIsPrevented()
    {
        _mockRepositoriesContext.CrewPartyCommandsMock.GetCrewParty().Should().BeNull();
    }

    [Then(@"the player receives a message indicating that the player already has an active Crew Party")]
    public void ThenThePlayerReceivesAMessageIndicatingThatThePlayerAlreadyHasAnActiveCrewParty()
    {
        _crewPartyCreationResultsContext.Exception.GetType().Should().Be<PlayerMultiplePartiesException>();
        _crewPartyCreationResultsContext.Exception.Message.Should().Be("A captain can only create one party at a time.");
    }
}