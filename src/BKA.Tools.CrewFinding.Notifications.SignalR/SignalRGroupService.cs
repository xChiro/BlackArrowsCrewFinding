using BKA.Tools.CrewFinding.Crews.Ports;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.SignalR.Management;

namespace BKA.Tools.CrewFinding.Notifications.SignalR;

public class SignalRGroupService(IDomainLogger logger, ServiceManager serviceManager) : ISignalRGroupService
{
    public void AddUserToGroupAsync(string connectionId, string groupName)
    {
        _ = Task.Run(async () =>
        {
            try
            {
                var hubContext = await serviceManager.CreateHubContextAsync("CrewHub", new CancellationToken());
                await hubContext.Groups.AddToGroupAsync(connectionId, groupName);
            }
            catch (Exception ex)
            {
                logger.Log(ex, $"Error adding user {connectionId} to SignalR group {groupName}");
            }
        });
    }

    public void SendMessageToGroupAsync<T>(string crewId, T message)
    {
        _ = Task.Run(async () =>
        {
            try
            {
                var hubContext = await serviceManager.CreateHubContextAsync("CrewHub", new CancellationToken());
                await hubContext.Clients.Group(crewId).SendAsync("NotifyPlayerJoined", message);
            }
            catch (Exception ex)
            {
                logger.Log(ex, $"Error sending message to SignalR group {crewId}");
            }
        });
    }
}