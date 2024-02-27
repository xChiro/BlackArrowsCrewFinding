using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Contexts;
using BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;
using BKA.Tools.CrewFinding.BehaviourTest.Globals;
using BKA.Tools.CrewFinding.CrewParties;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Steps;

[Binding]
public class CrewPartyAssertionsSteps
{
    private readonly CrewPartyContext _crewPartyContext;
    private readonly PlayerContext _playerContext;

    public CrewPartyAssertionsSteps(CrewPartyContext crewPartyContext, PlayerContext playerContext)
    {
        _playerContext = playerContext;
        _crewPartyContext = crewPartyContext;
    }
    
    [Then(@"a Crew Party named (.*) is successfully created")]
    public void When_thenACrewPartyNamedIsSuccessfullyCreated(string crewPartyDefaultName)
    {
        ICrewPartyCreator crewPartyCreator = new CrewPartyCreator(new CrewPartyCommandsMock(), 4);
        var crewPartyCreatorRequest = _crewPartyContext.ToRequest(_playerContext.UserName);
        
        crewPartyCreator.Create(crewPartyCreatorRequest);
    }
}