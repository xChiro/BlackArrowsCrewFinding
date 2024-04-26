using BKA.Tools.CrewFinding.BehaviourTest.Commons.Contexts;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class RecentCrewsArrangeSteps
{
    private readonly SystemSettingContext _systemSettingContext;

    public RecentCrewsArrangeSteps(SystemSettingContext systemSettingContext)
    {
        _systemSettingContext = systemSettingContext;
    }
    
    [Given(@"the system is configured to get the crews created in the last ""(.*)"" hours")]
    public void GivenTheSystemIsConfiguredToGetTheCrewsCreatedInTheLastHours(string hours)
    {
        _systemSettingContext.LeastCrewTimeThreshold = int.Parse(hours);
    }
}