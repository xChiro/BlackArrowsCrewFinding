using BKA.Tools.CrewFinding.CrewParties;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public class CrewPartyCommandsMock : ICrewPartyCommands
{
    public CrewParty CrewParty { get; private set; }
    public Captain Captain { get; private set; }
    
    public void SaveCrewParty(Captain captain, CrewParty crewParty)
    {
        Captain = captain;
        CrewParty = crewParty;
    }
}