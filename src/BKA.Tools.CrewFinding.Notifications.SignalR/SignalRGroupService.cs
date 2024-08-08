using BKA.Tools.CrewFinding.Crews.Ports;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.SignalR.Management;

namespace BKA.Tools.CrewFinding.Notifications.SignalR;

public class SignalRGroupService(IDomainLogger logger, ServiceManager serviceManager, string hubName)
    : ISignalRGroupService
{
    public void AddUserToGroupAsync(string userId, string groupName)
    {
        RunInHubContextAsync(hubContext => hubContext.UserGroups.AddToGroupAsync(userId, groupName),
            $"Error adding user {userId} to SignalR group {groupName}");
    }

    public void SendMessageToGroupAsync<T>(string groupName, T message, string methodName)
    {
        RunInHubContextAsync(hubContext => hubContext.Clients.Group(groupName).SendAsync(methodName, message),
            $"Error sending message to SignalR group {groupName}");
    }


    public void RemoveUserFromGroupAsync(string userId, string groupName)
    {
        RunInHubContextAsync(hubContext => hubContext.UserGroups.RemoveFromGroupAsync(userId, groupName),
            $"Error removing user {userId} from SignalR group {groupName}");
    }

    public void RemoveAllFromGroupAsync(string groupName)
    {
        RunInHubContextAsync(hubContext => hubContext.UserGroups.RemoveFromAllGroupsAsync(groupName),
            $"Error removing all users from SignalR group {groupName}");
    }

    public void SendMessageToUserIdAsync<TMessage>(string userId, TMessage message, string methodName)
    {
        RunInHubContextAsync(hubContext => hubContext.Clients.User(userId).SendAsync(methodName, message),
            $"Error sending message to SignalR user {userId}");
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