namespace BKA.Tools.CrewFinding.Crews.Commands.Leave;

public interface ICrewLeaver
{
    public Task Leave(ICrewLeaverResponse output);
}