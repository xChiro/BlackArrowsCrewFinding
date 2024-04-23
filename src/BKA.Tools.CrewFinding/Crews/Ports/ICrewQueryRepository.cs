namespace BKA.Tools.CrewFinding.Crews.Ports;

public interface ICrewQueryRepository
{
    public Task<bool> IsPlayerInActiveCrew(string playerId);
    
    public Task<Crew?> GetCrew(string crewId);
}