using BKA.Tools.CrewFinding.Crews.Commands.Kicks;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Crews;

public class MemberKickerSignalR(
    IMemberKicker decorated,
    IDomainLogger domainLogger,
    ISignalRGroupService signalRGroupService,
    ISignalRUserSession userSession) : IMemberKicker
{
    public async Task Kick(string memberId, IMemberKickerResponse output)
    {
        var memberKickerResponse = new KickMemberResponse();
        await decorated.Kick(memberId, memberKickerResponse);

        output.SetResponse(memberKickerResponse.CrewId);

        try
        {
            signalRGroupService.RemoveUserFromGroupAsync(userSession.GetConnectionId(), memberKickerResponse.CrewId);
            signalRGroupService.SendMessageToGroupAsync(memberKickerResponse.CrewId, memberKickerResponse, "CrewMemberKicked");
            signalRGroupService.SendMessageToUserAsync(userSession.GetConnectionId(), "You was kicked from the crew.", "CrewMemberKicked");
        }
        catch (Exception e)
        {
            domainLogger.Log(e, "Error kicking member");
        }
    }
}