using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;
using BKA.Tools.CrewFinding.Crews.Exceptions;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class CrewsRetrievalAssertSteps(CrewQueryResultContext crewQueryResultContext, CrewResponseMock crewResponseMock)
{
    [Then(@"I should see the following crews")]
    public void ThenIShouldSeeTheFollowingCrews(Table table)
    {
        foreach (var row in table.Rows)
        {
            crewQueryResultContext.Crews.Should()
                .Contain(c => c.Captain.CitizenName.Value == row["CaptainHandle"]
                              && c.Activity.Name == row["Activity"] &&
                              c.Activity.Description == row["Description"]
                              && c.Members.MaxSize == int.Parse(row["MaxCrewSize"]) &&
                              c.ReunionPoint.System == row["System"] &&
                              c.ReunionPoint.Place == row["Location"] &&
                              c.ReunionPoint.PlanetarySystem == row["PlanetarySystem"] &&
                              c.ReunionPoint.PlanetMoon == row["PlanetMoon"] &&
                              c.Members.Count() == Convert.ToInt32(row["CrewSize"])
                );
        }
    }

    [Then(@"I should get the following crew")]
    public void ThenIShouldGetTheFollowingCrew(Table table)
    {
        var firstRow = table.Rows.First();
        crewResponseMock.Crew!.Captain.CitizenName.Value.Should().Be(firstRow["CaptainHandle"]);
        crewResponseMock.Crew!.Activity.Name.Should().Be(firstRow["Activity"]);
        crewResponseMock.Crew!.Activity.Description.Should().Be(firstRow["Description"]);
        crewResponseMock.Crew!.Members.MaxSize.Should().Be(int.Parse(firstRow["MaxCrewSize"]));
        crewResponseMock.Crew!.ReunionPoint.System.Should().Be(firstRow["System"]);
        crewResponseMock.Crew!.ReunionPoint.Place.Should().Be(firstRow["Location"]);
        crewResponseMock.Crew!.ReunionPoint.PlanetarySystem.Should().Be(firstRow["PlanetarySystem"]);
        crewResponseMock.Crew!.ReunionPoint.PlanetMoon.Should().Be(firstRow["PlanetMoon"]);
        crewResponseMock.Crew!.Members.Count().Should().Be(Convert.ToInt32(firstRow["CurrentCrewSize"]));
    }

    [Then(@"I should get an error message indicating that the crew does not exist")]
    public void ThenIShouldGetAnErrorMessageIndicatingThatTheCrewDoesNotExist()
    {
        crewResponseMock.Crew.Should().BeNull();
        crewResponseMock.ExceptionResult.Should().BeOfType<CrewNotFoundException>();
    }
}