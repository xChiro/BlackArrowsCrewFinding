using BKA.Tools.CrewFinding.BehaviourTest.Crews.Contexts;
using BKA.Tools.CrewFinding.Crews.Exceptions;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Steps;

[Binding]
public class DisbandCrewAssertSteps(
    CrewRepositoriesContext crewRepositoriesContext,
    ExceptionResultContext exceptionResultContext,
    CrewDisbandmentResponseMock crewDisbandmentResponseMock)
{
    [Then(@"the Crew is disbanded successfully")]
    public void ThenTheCrewIsDisbandedSuccessfully()
    {
        var isDisbandedCrewIdInStoredCrews = crewRepositoriesContext.QueryRepositoryMock.StoredCrews
            .Any(crew => crew.Id == crewRepositoriesContext.CommandRepositoryMock.DisbandedCrewIds.FirstOrDefault());

        isDisbandedCrewIdInStoredCrews.Should().BeTrue();
        crewDisbandmentResponseMock.CrewId.Should()
            .Be(crewRepositoriesContext.CommandRepositoryMock.DisbandedCrewIds.FirstOrDefault());
    }

    [Then("the system notifies me that there is no Crew to disband")]
    [Then("the system does not allow me to disband the Crew")]
    public void ThenTheSystemNotifiesMeThatThereIsNoCrewToDisband()
    {
        exceptionResultContext.Exception.Should().BeOfType<CrewDisbandException>();
    }

    [Then(@"the following crewsId should be removed")]
    public void ThenTheFollowingCrewsIdShouldBeRemoved(Table table)
    {
        var crewIds = table.Rows.Select(row => row["CrewId"]).ToList();

        crewIds.Should().BeEquivalentTo(crewRepositoriesContext.CommandRepositoryMock.DisbandedCrewIds);
    }
}