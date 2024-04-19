namespace BKA.Tools.CrewFinding.Crews.Ports;

public interface ICrewQueries
{
    public Task<Crew?> GetCrewParty(string crewPartyId);
}