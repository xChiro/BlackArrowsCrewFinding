using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.Crews.Exceptions;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class CrewAssertSteps(
    CrewRepositoriesContext crewRepositoriesContext,
    CrewCreationResultsContext crewCreationResultsContext)
{
    [Then(@"an empty Crew named (.*) is successfully created")]
    public void ThenAnEmptyCrewNamedIsSuccessfullyCreated(string crewPartyName)
    {

        crewRepositoriesContext.CommandRepositoryMock.GetStoredCrew()?.Members.Should().BeEmpty();
        crewRepositoriesContext.CommandRepositoryMock.GetStoredCrew()?.Name.Value.Should()
            .Be(crewPartyName);
    }

    [Then(@"the Crew contains the following details:")]
    public void ThenTheCrewContainsTheFollowingDetails(Table table)
    {
        var crew = crewRepositoriesContext.CommandRepositoryMock.GetStoredCrew();
        
        crew.Should().NotBeNull();
        crew!.Activity.Description.Should().Be(table.Rows[0]["Description"]);
        crew.Activity.Name.Should().Be(table.Rows[0]["Activity"]);
        crew.ReunionPoint.Place.Should().Be(table.Rows[0]["Place"]);
        crew.ReunionPoint.PlanetMoon.Should().Be(table.Rows[0]["Planet/Moon"]);
        crew.ReunionPoint.PlanetarySystem.Should().Be(table.Rows[0]["PlanetarySystem"]);
        crew.ReunionPoint.System.Should().Be(table.Rows[0]["System"]);
        crew.Members.MaxSize.Should().Be(int.Parse(table.Rows[0]["CrewSize"]));
        
        var expectation = table.Rows[0]["Languages"].Split(',').Select(x => x.Trim()).ToList();
        crew.Languages.Select(language => language.LanguageCode).Should().BeEquivalentTo(expectation);

        crewCreationResultsContext.CrewCreatorResponseMock.Id.Should().NotBeNullOrEmpty();
    }

    [Then(@"the creation date is the current date")]
    public void ThenTheCreationDateIsTheCurrentDate()
    {
        crewRepositoriesContext.CommandRepositoryMock.GetStoredCrew()?.CreatedAt.Should()
            .BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Then(@"the Crew of (.*) is successfully created")]
    public void ThenTheCrewOfIsSuccessfullyCreated(string captainName)
    {
        crewRepositoriesContext.CommandRepositoryMock.GetStoredCrew().Should().NotBeNull();
        crewRepositoriesContext.CommandRepositoryMock.GetStoredCrew()!.Name.Value.Should()
            .Be($"Crew of {captainName}");
    }

    [Then(@"the MaxCrewSize is set to (.*)")]
    public void ThenTheMaxCrewSizeIsSetTo(string defaultMaxCrewSize)
    {
        var expected = int.Parse(defaultMaxCrewSize);
        
        crewRepositoriesContext.CommandRepositoryMock.GetStoredCrew()!.Members.MaxSize.Should().Be(expected);
    }

    [Then(@"the creation of the new Crew is prevented")]
    public void ThenTheCreationOfTheNewCrewIsPrevented()
    {
        crewRepositoriesContext.CommandRepositoryMock.GetStoredCrew().Should().BeNull();
    }

    [Then(@"the player receives a message indicating that the player already has an active Crew")]
    public void ThenThePlayerReceivesAMessageIndicatingThatThePlayerAlreadyHasAnActiveCrew()
    {
        crewCreationResultsContext.Exception.GetType().Should().Be<PlayerMultipleCrewsException>();
    }

    [Then(@"(.*) is designated as the Captain")]
    public void ThenIsDesignatedAsTheCaptain(string userName)
    {
        crewRepositoriesContext.CommandRepositoryMock.GetCaptain()!.CitizenName.Value.Should().Be(userName);
    }
}