using BKA.Tools.CrewFinding.CrewParties;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public class CrewPartyCommandsMock : ICrewPartyCommands
{
    private CrewParty? _crewParty;

    public Task CreateCrewParty(CrewParty crewParty)
    {
        _crewParty = crewParty;

        return Task.CompletedTask;
    }

    public Task UpdateMembers(string crewPartyId, IEnumerable<Player> crewPartyMembers)
    {
        throw new NotImplementedException();
    }

    public Player? GetCaptain()
    {
        return _crewParty?.Captain;
    }

    public CrewParty? GetCrewParty()
    {
        return _crewParty;
    }
}