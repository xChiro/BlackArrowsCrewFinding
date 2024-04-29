namespace BKA.Tools.CrewFinding.Crews.Leave;

public interface ICrewLeaver
{
    public Task Leave(string playerId, string crewId);
}