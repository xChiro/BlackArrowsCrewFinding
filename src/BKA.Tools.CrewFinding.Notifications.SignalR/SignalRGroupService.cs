using BKA.Tools.CrewFinding.Crews.Ports;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.SignalR.Management;

namespace BKA.Tools.CrewFinding.Notifications.SignalR;

public class SignalRGroupService(IDomainLogger logger, ServiceManager serviceManager, string hubName) : ISignalRGroupService
{
    public void AddUserToGroupAsync(string connectionId, string groupName)
    {
        _ = Task.Run(async () =>
        {
            try
            {
                var hubContext = await serviceManager.CreateHubContextAsync(hubName, new CancellationToken());
                await hubContext.Groups.AddToGroupAsync(connectionId, groupName);
            }
            catch (Exception ex)
            {
                logger.Log(ex, $"Error adding user {connectionId} to SignalR group {groupName}");
            }
        });
    }

    public void SendMessageToGroupAsync<T>(string groupName, T message, string methodName)
    {
        _ = Task.Run(async () =>
        {
            try
            {
                var hubContext = await serviceManager.CreateHubContextAsync(hubName, new CancellationToken());
                await hubContext.Clients.Group(groupName).SendAsync(methodName, message);
            }
            catch (Exception ex)
            {
                logger.Log(ex, $"Error sending message to SignalR group {groupName}");
            }
        });
    }

    public void RemoveUserFromGroupAsync(string connectionId, string groupName)
    {
        _ = Task.Run(async () =>
        {
            try
            {
                var hubContext = await serviceManager.CreateHubContextAsync(hubName, new CancellationToken());
                await hubContext.Groups.RemoveFromGroupAsync(connectionId, groupName);
            }
            catch (Exception ex)
            {
                logger.Log(ex, $"Error removing user {connectionId} from SignalR group {groupName}");
            }
        });
    }
}