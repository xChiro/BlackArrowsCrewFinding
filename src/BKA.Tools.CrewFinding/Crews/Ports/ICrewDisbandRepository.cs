namespace BKA.Tools.CrewFinding.Crews.Ports;

public interface ICrewDisbandRepository
{
    public Task Disband(string crewId);
}