using BKA.Tools.CrewFinding.Crews.Commands.Kicks;

namespace BKA.Tools.CrewFinding.API.Functions.Crews.Commands.KickMember;

public class KickMemberResponse : IMemberKickerResponse
{
    public string CrewId { get; private set; } = string.Empty;
    
    public void SetResponse(string crewId)
    {
        CrewId = crewId;
    }
}