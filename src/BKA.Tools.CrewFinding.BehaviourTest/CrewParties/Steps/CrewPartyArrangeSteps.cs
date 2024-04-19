using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class CrewPartyArrangeSteps

{
    private readonly CrewPartyContext _crewPartyContext;
    private readonly CrewPartyRepositoriesContext _crewPartyRepositoriesContext;

    public CrewPartyArrangeSteps(CrewPartyContext crewPartyContext, CrewPartyRepositoriesContext crewPartyRepositoriesContext)
    {
        _crewPartyContext = crewPartyContext;
        _crewPartyRepositoriesContext = crewPartyRepositoriesContext;
    }

    [Given(@"the default MaxCrewSize is (.*)")]
    public void GivenTheDefaultMaxCrewSizeIs(string defaultMaxCrewSize)
    {
        _crewPartyContext.MaxPlayerAllowed = int.Parse(defaultMaxCrewSize);
    }

    [Given(@"the player already has an active Crew Party")]
    public void GivenThePlayerAlreadyHasAnActiveCrewParty()
    {
        _crewPartyRepositoriesContext.CrewPartyQueriesMocks = new CrewPartyQueriesMock(true);
    }
}