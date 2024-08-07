using BKA.Tools.CrewFinding.Crews.Commands.JoinRequests;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Crews;

public class CrewJoinerSignalR(
    ICrewJoiner crewJoinerMock,
    ISignalRGroupService crewHubContext,
    ISignalRUserSession userSession,
    IDomainLogger domainLogger)
    : ICrewJoiner
{
    public async Task Join(string crewId)
    {
        await crewJoinerMock.Join(crewId);

        try
        {
            crewHubContext.AddUserToGroupAsync(userSession.GetConnectionId(), crewId);
            crewHubContext.SendMessageToGroupAsync(crewId, new
                {
                    Type = "NotifyPlayerJoined",
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