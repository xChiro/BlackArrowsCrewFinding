using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class RecentCrewsArrangeSteps
{
    private readonly CrewRepositoriesContext _crewRepositoriesContext;

    public RecentCrewsArrangeSteps(CrewRepositoriesContext crewRepositoriesContext)
    {
        _crewRepositoriesContext = crewRepositoriesContext;
    }
    
    [Given(@"the system is configured to get the crews created in the last ""(.*)"" hours")]
    public void GivenTheSystemIsConfiguredToGetTheCrewsCreatedInTheLastHours(string p0)
    {
        
    }
}