using BKA.Tools.CrewFinding.BehaviourTest.Commons.Contexts;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class RecentCrewsArrangeSteps(SystemSettingContext systemSettingContext)
{
    [Given(@"the system is configured to get the crews created in the last ""(.*)"" hours")]
    public void GivenTheSystemIsConfiguredToGetTheCrewsCreatedInTheLastHours(string hours)
    {
        systemSettingContext.LeastCrewTimeThreshold = int.Parse(hours);
    }
}