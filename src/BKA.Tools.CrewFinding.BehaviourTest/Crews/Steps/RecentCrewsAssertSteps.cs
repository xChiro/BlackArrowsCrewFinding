using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class RecentCrewsAssertSteps
{
    private readonly CrewQueryResultContext _crewQueryResultContext;

    public RecentCrewsAssertSteps(CrewQueryResultContext crewQueryResultContext)
    {
        _crewQueryResultContext = crewQueryResultContext;
    }

    [Then(@"I should see the following crews")]
    public void ThenIShouldSeeTheFollowingCrews(Table table)
    {
        foreach (var row in table.Rows)
        {
            _crewQueryResultContext.Crews.Should()
                .Contain(c => c.Captain.CitizenName == row["CaptainHandle"]
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
}