using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class CrewPartyArrangeSteps

{
    private readonly CrewPartyContext _crewPartyContext;
    private readonly MockRepositoriesContext _mockRepositoriesContext;

    public CrewPartyArrangeSteps(CrewPartyContext crewPartyContext, MockRepositoriesContext mockRepositoriesContext)
    {
        _crewPartyContext = crewPartyContext;
        _mockRepositoriesContext = mockRepositoriesContext;
    }

    [Given(@"the default MaxCrewSize is (.*)")]
    public void GivenTheDefaultMaxCrewSizeIs(string defaultMaxCrewSize)
    {
        _crewPartyContext.MaxPlayerAllowed = int.Parse(defaultMaxCrewSize);
    }

    [Given(@"the player already has an active Crew Party")]
    public void GivenThePlayerAlreadyHasAnActiveCrewParty()
    {
        _mockRepositoriesContext.CrewPartyQueriesMocks = new CrewPartyQueriesMock(true);
    }
}