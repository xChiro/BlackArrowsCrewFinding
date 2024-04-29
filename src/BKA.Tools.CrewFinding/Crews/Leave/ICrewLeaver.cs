namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Leave;

public interface ICrewLeaver
{
    public Task Leave(string playerId, string crewId);
}