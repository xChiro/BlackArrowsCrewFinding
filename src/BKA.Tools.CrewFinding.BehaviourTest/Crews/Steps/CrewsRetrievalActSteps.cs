using BKA.Tools.CrewFinding.BehaviourTest.Commons.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;
using BKA.Tools.CrewFinding.Crews.Queries.Recent;
using BKA.Tools.CrewFinding.Crews.Queries.Retrievs;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class CrewsRetrievalActSteps(
    CrewRepositoriesContext crewRepositoriesContext,
    SystemSettingContext systemSettingContext,
    CrewQueryResultContext crewQueryResultContext,
    CrewResponseMock crewResponseMock)
{
    [When(@"I view the recently created crews")]
    public async Task WhenIViewTheRecentlyCreatedCrews()
    {
        var sut = new RecentCrewsRetrieval(crewRepositoriesContext.QueryRepositoryMock,
            systemSettingContext.LeastCrewTimeThreshold);

        await sut.Retrieve(crewQueryResultContext);
    }

    [When(@"I want to obtain the crew with identification code ""(.*)""")]
    public async Task WhenIWantToObtainTheCrewWithIdentificationCode(string crewId)
    {
        var sut = new ActiveCrewRetrieval(crewRepositoriesContext.QueryRepositoryMock);
        await sut.Retrieve(crewId, crewResponseMock);
    }

    [When(@"I attempt to obtain the crew with identification code ""(.*)""")]
    public async Task WhenIAttemptToObtainTheCrewWithIdentificationCode(string crewId)
    {
        var sut = new ActiveCrewRetrieval(crewRepositoriesContext.QueryRepositoryMock);

        try
        {
            await sut.Retrieve(crewId, crewResponseMock);
        }
        catch (Exception e)
        {
            crewResponseMock.SetException(e);   
        }
    }
}