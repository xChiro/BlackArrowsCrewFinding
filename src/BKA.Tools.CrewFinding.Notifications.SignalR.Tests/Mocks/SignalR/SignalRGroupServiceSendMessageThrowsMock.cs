namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.SignalR;

public class SignalRGroupServiceSendMessageThrowsMock<T> : ISignalRGroupService where T : Exception, new()
{
    public string UserId { get; private set; } = string.Empty;
    public string GroupName { get; private set; } = string.Empty;

    public void AddUserToGroupAsync(string userId, string groupName)
    {
        UserId = userId;
        GroupName = groupName;
    }

    public void SendMessageToGroupAsync<TMessage>(string groupName, TMessage message, string methodName)
    {
        throw new T();
    }

    public void RemoveConnectionFromGroupAsync(string connectionId, string groupName)
    {
        UserId = connectionId;
        GroupName = groupName;
    }

    public void RemoveUserFromGroupAsync(string userId, string groupName)
    {
        UserId = userId;
        GroupName = groupName;
    }

    public void RemoveAllFromGroupAsync(string groupName)
    {
        GroupName = groupName;
    }

    public void SendMessageToUserAsync<TMessage>(string userId, TMessage message, string methodName)
    {
        throw new T();
    }
}