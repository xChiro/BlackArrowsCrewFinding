using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class CrewArrangeSteps

{
    private readonly CrewContext _crewContext;
    private readonly CrewRepositoriesContext _crewRepositoriesContext;

    public CrewArrangeSteps(CrewContext crewContext, CrewRepositoriesContext crewRepositoriesContext)
    {
        _crewContext = crewContext;
        _crewRepositoriesContext = crewRepositoriesContext;
    }

    [Given(@"the default MaxCrewSize is (.*)")]
    public void GivenTheDefaultMaxCrewSizeIs(string defaultMaxCrewSize)
    {
        _crewContext.MaxPlayerAllowed = int.Parse(defaultMaxCrewSize);
    }

    [Given(@"the player already has an active Crew")]
    public void GivenThePlayerAlreadyHasAnActiveCrew()
    {
        _crewRepositoriesContext.CrewQueriesMocks = new CrewQueriesMock(true);
    }
}