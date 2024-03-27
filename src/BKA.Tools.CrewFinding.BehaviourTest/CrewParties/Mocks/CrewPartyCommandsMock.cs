using BKA.Tools.CrewFinding.CrewParties;
using BKA.Tools.CrewFinding.CrewParties.Ports;
using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.BehaviourTest.CrewParties.Mocks;

public class CrewPartyCommandsMock : ICrewPartyCommands
{
    private Player? _captain;
    private CrewParty? _crewParty;

    public string SaveCrewParty(Player captain, CrewParty crewParty)
    {
        _captain = captain;
        _crewParty = crewParty;

        return Guid.NewGuid().ToString();
    }

    public Player? GetCaptain()
    {
        return _captain;
    }

    public CrewParty? GetCrewParty()
    {
        return _crewParty;
    }
}