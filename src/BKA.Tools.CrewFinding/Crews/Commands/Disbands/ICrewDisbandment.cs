namespace BKA.Tools.CrewFinding.Crews.Commands.Disbands;

public interface ICrewDisbandment
{
    public Task Disband(string crewId);
}