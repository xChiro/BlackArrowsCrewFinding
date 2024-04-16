namespace BKA.Tools.CrewFinding.CrewParties.Ports;

public interface ICrewPartyQueries
{
    public Task<bool> IsPlayerInAParty(string playerId);
    public Task<CrewParty?> GetCrewParty(string crewPartyId);
}