namespace BKA.Tools.CrewFinding.Ports;

public interface ICrewPartyQueries
{
    public Task<bool> PlayerHasCreatedParty(string captainId);
}