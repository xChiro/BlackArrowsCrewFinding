namespace BKA.Tools.CrewFinding.Crews.Ports;

public interface ICrewQueryRepository
{
    public Task<Crew?> GetCrew(string crewId);
    
    public Task<Crew[]> GetCrews(DateTime from, DateTime to);
}