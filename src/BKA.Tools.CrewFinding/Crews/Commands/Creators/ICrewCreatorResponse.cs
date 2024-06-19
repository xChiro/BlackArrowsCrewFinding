namespace BKA.Tools.CrewFinding.Crews.Commands.Creators;

public interface ICrewCreatorResponse
{
    public string GetName();
    
    public void SetResponse(string id, string name);
}