namespace BKA.Tools.CrewFinding.Crews.Commands.Leave;

public interface ICrewLeaver
{
    public Task Leave(string playerId, string crewId);
}