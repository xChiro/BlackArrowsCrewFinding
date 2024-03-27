using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.CrewParties.Ports;

public interface ICrewPartyCommands
{
    public string SaveCrewParty(Player captain, CrewParty crewParty);
}