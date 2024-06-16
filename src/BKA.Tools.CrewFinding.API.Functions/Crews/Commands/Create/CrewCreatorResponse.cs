using BKA.Tools.CrewFinding.Crews.Commands.Creators;

namespace BKA.Tools.CrewFinding.API.Functions.Crews.Commands.Create;

public class CrewCreatorResponse : ICrewCreatorResponse
{
    public string CrewId { get; private set; }

    public void SetResponse(string id)
    {
        CrewId = id;
    }
}