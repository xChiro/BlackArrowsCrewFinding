namespace BKA.Tools.CrewFinding.CrewParties.Ports;

public interface ICrewPartyQueries
{
    public Task<bool> CaptainHasCreatedParty(string captainName);
}