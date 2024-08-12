using BKA.Tools.CrewFinding.Crews.Commands.Leave;

namespace BKA.Tools.CrewFinding.API.Functions.Crews.Commands.Leave;

public class LeaveCrewFunctionResponse : ICrewLeaverResponse
{
    public string CrewId { get; set; }
    
    public void SetResponse(string crewId)
    {
        CrewId = crewId;
    }
}