namespace BKA.Tools.CrewFinding.Crews.Disbands;

public interface ICrewDisbandment
{
    public Task Disband(string crewId);
}