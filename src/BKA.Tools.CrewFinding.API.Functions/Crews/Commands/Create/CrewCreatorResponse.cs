using BKA.Tools.CrewFinding.Crews.Commands.Creators;

namespace BKA.Tools.CrewFinding.API.Functions.Crews.Commands.Create;

public class CrewCreatorResponse : ICrewCreatorResponse
{
    private string _crewName = string.Empty;
    
    public string CrewId { get; private set; } = string.Empty;

    public string GetName()
    {
        return _crewName;
    }

    public void SetResponse(string id, string name)
    {
        CrewId = id;
        _crewName = name;
    }
}