using BKA.Tools.CrewFinding.Crews.Commands.Kicks;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Crews;

public class MemberKickerSignalR(
    IMemberKicker decorated,
    IDomainLogger domainLogger,
    ISignalRGroupService signalRGroupService) : IMemberKicker
{
    public async Task Kick(string memberId, IMemberKickerResponse output)
    {
        var memberKickerResponse = new KickMemberResponse();
        await decorated.Kick(memberId, memberKickerResponse);

        output.SetResponse(memberKickerResponse.CrewId);

        try
        {
            signalRGroupService.RemoveUserFromGroupAsync(memberId, memberKickerResponse.CrewId);
            signalRGroupService.SendMessageToUserAsync(memberId, "You have been kicked from the crew.", "KickedFromCrew");
            signalRGroupService.SendMessageToGroupAsync(memberKickerResponse.CrewId, new
            {
                memberKickerResponse.CrewId,
                PlayerId = memberId
            }, "CrewMemberKicked");
        }
        catch (Exception e)
        {
            domainLogger.Log(e, "Error kicking member");
        }
    }
}