using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Commands.JoinRequests;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Crews;

public class CrewJoinerSignalR(
    ICrewJoiner decorated,
    ISignalRGroupService crewHubContext,
    IUserSession userSession,
    IDomainLogger domainLogger)
    : ICrewJoiner
{
    public async Task Join(string crewId)
    {
        await decorated.Join(crewId);

        try
        {
            crewHubContext.AddUserToGroupAsync(userSession.GetUserId(), crewId);
            crewHubContext.SendMessageToGroupAsync(crewId, new
                {
                    CrewId = crewId,
                    PlayerId = userSession.GetUserId()
                },
                "NotifyPlayerJoined");
        }
        catch (Exception e)
        {
            domainLogger.Log(e,
                $"Error join to SignalR group with crewId: {crewId}, playerId: {userSession.GetUserId()}");
        }
    }
}