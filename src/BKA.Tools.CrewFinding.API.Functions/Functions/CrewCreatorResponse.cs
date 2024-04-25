using BKA.Tools.CrewFinding.Crews.CreateRequests;

namespace BKA.Tools.CrewFinding.API.Functions.Functions;

public class CrewCreatorResponse : ICrewCreatorResponse
{
    public string CrewId { get; private set; }

    public void SetResponse(string id)
    {
        CrewId = id;
    }
}