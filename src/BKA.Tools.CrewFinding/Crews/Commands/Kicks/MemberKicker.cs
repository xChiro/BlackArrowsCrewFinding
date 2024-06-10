using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Crews.Commands.Kicks;

public class MemberKicker(IUserSession userSession, ICrewQueryRepository crewQueryRepository, ICrewCommandRepository
    crewCommandRepository) : IMemberKicker
{
    public async Task Kick(string memberId)
    {
        var captainId = userSession.GetUserId();
        var crew = await crewQueryRepository.GetActiveCrewByPlayerId(captainId);

        if (IsTheCaptain(crew, captainId))
            throw new NotCaptainException();

        if(!crew!.RemoveMember(memberId))
            throw new NoCrewMemberException();

        await crewCommandRepository.UpdateMembers(crew.Id, crew.Members);
    }
    
    private static bool IsTheCaptain(Crew? crew, string captainId)
    {
        return crew == null || crew.Captain.Id != captainId;
    }
}