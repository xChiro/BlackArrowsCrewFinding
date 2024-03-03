using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.CrewParties;

public interface ICrewPartyCommands
{
    public void SaveCrewParty(Captain captain, CrewParty crewParty);
}