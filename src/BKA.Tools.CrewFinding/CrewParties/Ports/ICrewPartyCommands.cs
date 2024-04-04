namespace BKA.Tools.CrewFinding.CrewParties.Ports;

public interface ICrewPartyCommands
{
    public Task CreateCrewParty(CrewParty crewParty);
    
    public Task AddPlayerToCrewParty(string playerId, string crewPartyId);
}