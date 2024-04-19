using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.Crews.Exceptions;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class CrewAssertSteps
{
    private readonly CrewRepositoriesContext _crewRepositoriesContext;
    private readonly CrewCreationResultsContext _crewCreationResultsContext;

    public CrewAssertSteps(CrewRepositoriesContext crewRepositoriesContext, 
        CrewCreationResultsContext crewCreationResultsContext)
    {
        _crewRepositoriesContext = crewRepositoriesContext;
        _crewCreationResultsContext = crewCreationResultsContext;
    }

    [Then(@"an empty Crew named (.*) is successfully created")]
    public void ThenAnEmptyCrewNamedIsSuccessfullyCreated(string crewPartyName)
    {

        _crewRepositoriesContext.CrewCommandsMock.GetStoredCrew()?.Members.Should().BeEmpty();
        _crewRepositoriesContext.CrewCommandsMock.GetStoredCrew()?.Name.Value.Should()
            .Be(crewPartyName);
    }

    [Then(@"the Crew contains the following details:")]
    public void ThenTheCrewContainsTheFollowingDetails(Table table)
    {
        var crew = _crewRepositoriesContext.CrewCommandsMock.GetStoredCrew();
        
        crew.Should().NotBeNull();
        crew!.Activity.Description.Should().Be(table.Rows[0]["Description"]);
        crew.Activity.Name.Should().Be(table.Rows[0]["Activity"]);
        crew.ReunionPoint.Place.Should().Be(table.Rows[0]["Place"]);
        crew.ReunionPoint.PlanetMoon.Should().Be(table.Rows[0]["Planet/Moon"]);
        crew.ReunionPoint.PlanetarySystem.Should().Be(table.Rows[0]["PlanetarySystem"]);
        crew.ReunionPoint.System.Should().Be(table.Rows[0]["System"]);
        crew.Members.MaxAllowed.Should().Be(int.Parse(table.Rows[0]["CrewSize"]));
        
        var expectation = table.Rows[0]["Languages"].Split(',').Select(x => x.Trim()).ToList();
        crew.Languages.Select(language => language.LanguageCode).Should().BeEquivalentTo(expectation);

        _crewCreationResultsContext.CrewCreatorResponseMock.Id.Should().NotBeNullOrEmpty();
    }

    [Then(@"the creation date is the current date")]
    public void ThenTheCreationDateIsTheCurrentDate()
    {
        _crewRepositoriesContext.CrewCommandsMock.GetStoredCrew()?.CreationAt.Should()
            .BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Then(@"the Crew of (.*) is successfully created")]
    public void ThenTheCrewOfIsSuccessfullyCreated(string captainName)
    {
        _crewRepositoriesContext.CrewCommandsMock.GetStoredCrew().Should().NotBeNull();
        _crewRepositoriesContext.CrewCommandsMock.GetStoredCrew()!.Name.Value.Should()
            .Be($"Crew of {captainName}");
    }

    [Then(@"the MaxCrewSize is set to (.*)")]
    public void ThenTheMaxCrewSizeIsSetTo(string defaultMaxCrewSize)
    {
        var expected = int.Parse(defaultMaxCrewSize);
        
        _crewRepositoriesContext.CrewCommandsMock.GetStoredCrew()!.Members.MaxAllowed.Should().Be(expected);
    }

    [Then(@"the creation of the new Crew is prevented")]
    public void ThenTheCreationOfTheNewCrewIsPrevented()
    {
        _crewRepositoriesContext.CrewCommandsMock.GetStoredCrew().Should().BeNull();
    }

    [Then(@"the player receives a message indicating that the player already has an active Crew")]
    public void ThenThePlayerReceivesAMessageIndicatingThatThePlayerAlreadyHasAnActiveCrew()
    {
        _crewCreationResultsContext.Exception.GetType().Should().Be<PlayerMultipleCrewsException>();
    }

    [Then(@"(.*) is designated as the Captain")]
    public void ThenIsDesignatedAsTheCaptain(string userName)
    {
        _crewRepositoriesContext.CrewCommandsMock.GetCaptain()!.CitizenName.Should().Be(userName);
    }
}