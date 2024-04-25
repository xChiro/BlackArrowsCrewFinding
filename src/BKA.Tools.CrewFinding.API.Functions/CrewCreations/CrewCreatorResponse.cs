using BKA.Tools.CrewFinding.Crews.CreateRequests;

namespace BKA.Tools.CrewFinding.API.Functions.CrewCreations;

public class CrewCreatorResponse : ICrewCreatorResponse
{
    public string CrewId { get; private set; }

    public void SetResponse(string id)
    {
        CrewId = id;
    }
}