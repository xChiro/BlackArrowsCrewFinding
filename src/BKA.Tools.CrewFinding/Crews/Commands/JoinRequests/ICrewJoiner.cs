namespace BKA.Tools.CrewFinding.Crews.Commands.JoinRequests;

public interface ICrewJoiner
{
    public Task Join(string crewId);
}