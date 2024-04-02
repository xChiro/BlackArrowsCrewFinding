using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.CrewParties.Ports;

public interface ICrewPartyCommands
{
    public Task<string> SaveCrewParty(Player captain, CrewParty crewParty);
    
    public Task AddPlayerToCrewParty(string playerId, string crewPartyId);
}