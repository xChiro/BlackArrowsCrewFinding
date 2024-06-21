using BKA.Tools.CrewFinding.Crews.Commands.Disbands;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;

public class CrewDisbandmentResponseMock : ICrewDisbandmentResponse
{
    public string CrewId { get; set; } = string.Empty;
    
    public void SetResult(string crewId)
    {
        CrewId = crewId;
    }
}