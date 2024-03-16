using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class CrewPartyArrangeSteps

{
    private readonly CrewPartyContext _crewPartyContext;

    public CrewPartyArrangeSteps(CrewPartyContext crewPartyContext)
    {
        _crewPartyContext = crewPartyContext;
    }

    [Given(@"the default MaxCrewSize is (.*)")]
    public void GivenTheDefaultMaxCrewSizeIs(string defaultMaxCrewSize)
    {
        _crewPartyContext.MaxPlayerAllowed = int.Parse(defaultMaxCrewSize);
    }
}