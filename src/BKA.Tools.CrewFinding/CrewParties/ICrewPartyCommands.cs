using BKA.Tools.CrewFinding.CrewParties.Values;

namespace BKA.Tools.CrewFinding.CrewParties;

public interface ICrewPartyCommands
{
    public void SaveCrewParty(Captain captain, CrewParty crewParty);
}