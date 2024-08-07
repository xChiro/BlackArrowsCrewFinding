using BKA.Tools.CrewFinding.Crews.Ports;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.SignalR.Management;

namespace BKA.Tools.CrewFinding.Notifications.SignalR;

public class SignalRGroupService(IDomainLogger logger, ServiceManager serviceManager, string hubName)
    : ISignalRGroupService
{
    public void AddUserToGroupAsync(string connectionId, string groupName)
    {
        RunInHubContextAsync(hubContext => hubContext.Groups.AddToGroupAsync(connectionId, groupName),
            $"Error adding user {connectionId} to SignalR group {groupName}");
    }

    public void SendMessageToGroupAsync<T>(string groupName, T message, string methodName)
    {
        RunInHubContextAsync(hubContext => hubContext.Clients.Group(groupName).SendAsync(methodName, message),
            $"Error sending message to SignalR group {groupName}");
    }

    public void RemoveUserFromGroupAsync(string connectionId, string groupName)
    {
        RunInHubContextAsync(hubContext => hubContext.Groups.RemoveFromGroupAsync(connectionId, groupName),
            $"Error removing user {connectionId} from SignalR group {groupName}");
    }

    public void RemoveAllFromGroupAsync(string groupName)
    {
        RunInHubContextAsync(hubContext => hubContext.Groups.RemoveFromAllGroupsAsync(groupName),
            $"Error removing all users from SignalR group {groupName}");
    }

    public void SendMessageToUserAsync(string connectionId, string message, string methodName)
    {
        RunInHubContextAsync(hubContext => hubContext.Clients.Client(connectionId).SendAsync(methodName, message),
            $"Error sending message to SignalR user {connectionId}");
    }

    private void RunInHubContextAsync(Func<ServiceHubContext, Task> action, string errorMessage)
    {
        _ = Task.Run(async () =>
        {
            try
            {
                var hubContext = await serviceManager.CreateHubContextAsync(hubName, new CancellationToken());
                await action(hubContext);
            }
            catch (Exception ex)
            {
                logger.Log(ex, errorMessage);
            }
        });
    }
}