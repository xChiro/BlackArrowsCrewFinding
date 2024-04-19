namespace BKA.Tools.CrewFinding.Crews.Ports;

public interface ICrewQueries
{
    public Task<bool> PlayerAlreadyInACrew(string captainId);
    public Task<Crew?> GetCrewParty(string crewPartyId);
}