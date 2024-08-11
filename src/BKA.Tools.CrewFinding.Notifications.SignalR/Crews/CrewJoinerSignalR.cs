using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Commands.JoinRequests;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Crews;

public class CrewJoinerSignalR(
    ICrewJoiner decorated,
    ISignalRGroupService crewHubContext,
    IPlayerQueryRepository playerQueryRepository,
    IUserSession userSession,
    IDomainLogger domainLogger)
    : ICrewJoiner
{
    public async Task Join(string crewId)
    {
        await decorated.Join(crewId);

        try
        {
            var getPlayerTask = playerQueryRepository.GetPlayer(userSession.GetUserId());
            crewHubContext.AddUserToGroupAsync(userSession.GetUserId(), crewId);

            var player = await getPlayerTask;
            crewHubContext.SendMessageToGroupAsync(crewId, new
                {
                    PlayerId = userSession.GetUserId(),
                    CitizenName = player?.CitizenName.Value
                },
                "NotifyPlayerJoined", [userSession.GetUserId()]);
        }
        catch (Exception e)
        {
            domainLogger.Log(e,
                $"Error join to SignalR group with crewId: {crewId}, playerId: {userSession.GetUserId()}");
        }
    }
}