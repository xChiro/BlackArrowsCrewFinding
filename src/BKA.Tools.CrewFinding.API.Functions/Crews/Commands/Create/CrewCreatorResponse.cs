using BKA.Tools.CrewFinding.Crews.Commands.Creators;

namespace BKA.Tools.CrewFinding.API.Functions.Crews.Commands.Create;

public class CrewCreatorResponse : ICrewCreatorResponse
{
    public string Id { get; private set; } = string.Empty;

    public void SetResponse(string id)
    {
        Id = id;
    }
}