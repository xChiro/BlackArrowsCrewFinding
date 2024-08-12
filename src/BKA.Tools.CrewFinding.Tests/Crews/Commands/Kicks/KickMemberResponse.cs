using BKA.Tools.CrewFinding.Crews.Commands.Kicks;

namespace BKA.Tools.CrewFinding.Tests.Crews.Commands.Kicks;

public class KickMemberResponse : IMemberKickerResponse
{
    public string CrewId { get; private set; }
    
    public void SetResponse(string crewId)
    {
        CrewId = crewId;
    }
}