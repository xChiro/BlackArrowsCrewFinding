namespace BKA.Tools.CrewFinding.CrewParties.Ports;

public interface ICrewPartyQueries
{
    public Task<bool> PlayerAlreadyInAParty(string captainId);
    public Task<CrewParty?> GetCrewParty(string crewPartyId);
}