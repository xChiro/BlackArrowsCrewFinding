using BKA.Tools.CrewFinding.BehaviourTest.Commons.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.Crews.Queries.Recents;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class RecentCrewsActSteps
{
    private readonly CrewRepositoriesContext _crewRepositoriesContext;
    private readonly SystemSettingContext _systemSettingContext;
    private readonly CrewQueryResultContext _crewQueryResultContext;

    public RecentCrewsActSteps(CrewRepositoriesContext crewRepositoriesContext,
        SystemSettingContext systemSettingContext, CrewQueryResultContext crewQueryResultContext)
    {
        _crewRepositoriesContext = crewRepositoriesContext;
        _systemSettingContext = systemSettingContext;
        _crewQueryResultContext = crewQueryResultContext;
    }

    [When(@"I view the recently created crews")]
    public async Task WhenIViewTheRecentlyCreatedCrews()
    {
        var sut = new RecentCrewsRetrieval(_crewRepositoriesContext.QueryRepositoryMock,
            _systemSettingContext.LeastCrewTimeThreshold);

        await sut.Retrieve(_crewQueryResultContext);
    }
}