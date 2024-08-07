using BKA.Tools.CrewFinding.Crews.Commands.Disbands;

namespace BKA.Tools.CrewFinding.API.Functions.Crews.Commands.Disband;

public class CrewDisbandmentResponse : ICrewDisbandmentResponse
{
    public string CrewId { get; private set; } = string.Empty;
    
    public void SetResult(string crewId)
    {
        CrewId = crewId;
    }
}