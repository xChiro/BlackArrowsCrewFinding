namespace BKA.Tools.CrewFinding.Crews.Ports;

public interface ICrewQueryRepository
{
    Task<Crew?> GetCrew(string crewId);
    
    Task<Crew[]> GetCrews(DateTime from, DateTime to);
}