using BKA.Tools.CrewFinding.CrewParties;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public class CrewPartyCommandsMock : ICrewPartyCommands
{
    private CrewParty? _crewPartyCreated;
    public Player[] CrewPartyMembers { get; private set; }
    
    public Task CreateCrewParty(CrewParty crewParty)
    {
        _crewPartyCreated = crewParty;

        return Task.CompletedTask;
    }

    public Task UpdateMembers(string crewPartyId, IEnumerable<Player> crewPartyMembers)
    {
        CrewPartyMembers = crewPartyMembers.ToArray();
        
        return Task.CompletedTask;
    }

    public Player? GetCaptain()
    {
        return _crewPartyCreated?.Captain;
    }

    public CrewParty? GetCrewParty()
    {
        return _crewPartyCreated;
    }
}