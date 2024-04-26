using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.Crews.Exceptions;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class DisbandCrewAssertSteps
{
    private readonly CrewRepositoriesContext _crewRepositoriesContext;
    private readonly ExceptionResultContext _exceptionResultContext;

    public DisbandCrewAssertSteps(CrewRepositoriesContext crewRepositoriesContext,
        ExceptionResultContext exceptionResultContext)
    {
        _crewRepositoriesContext = crewRepositoriesContext;
        _exceptionResultContext = exceptionResultContext;
    }

    [Then(@"the Crew is disbanded successfully")]
    public void ThenTheCrewIsDisbandedSuccessfully()
    {
        var isDisbandedCrewIdInStoredCrews = _crewRepositoriesContext.CrewValidationRepositoryMocks.StoredCrews
            .Any(crew => crew.Id == _crewRepositoriesContext.CrewCommandRepositoryMock.DisbandedCrewId);

        isDisbandedCrewIdInStoredCrews.Should().BeTrue();
    }

    [Then("the system notifies me that there is no Crew to disband")]
    [Then("the system does not allow me to disband the Crew")]
    public void ThenTheSystemNotifiesMeThatThereIsNoCrewToDisband()
    {
        _exceptionResultContext.Exception.Should().BeOfType<CrewDisbandException>();
    }
}