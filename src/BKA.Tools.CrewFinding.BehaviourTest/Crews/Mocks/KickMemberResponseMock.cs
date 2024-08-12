using BKA.Tools.CrewFinding.Crews.Commands.Kicks;

namespace BKA.Tools.CrewFinding.BehaviourTest.Crews.Mocks;

public class KickMemberResponseMock : IMemberKickerResponse
{
    public string CrewId { get; private set; } = string.Empty;
    
    public void SetResponse(string crewId)
    {
        CrewId = crewId;
    }
}