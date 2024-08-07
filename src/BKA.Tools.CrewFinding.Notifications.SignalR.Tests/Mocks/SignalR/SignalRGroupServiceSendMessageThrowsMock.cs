namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.SignalR;

public class SignalRGroupServiceSendMessageThrowsMock<T> : ISignalRGroupService where T : Exception, new()
{
    public string ConnectionId { get; private set; } = string.Empty;
    public string GroupName { get; private set; } = string.Empty;

    public void AddUserToGroupAsync(string connectionId, string groupName)
    {
        ConnectionId = connectionId;
        GroupName = groupName;
    }

    public void SendMessageToGroupAsync<TMessage>(string groupName, TMessage message, string methodName)
    {
        throw new T();
    }

    public void RemoveUserFromGroupAsync(string connectionId, string groupName)
    {
        ConnectionId = connectionId;
        GroupName = groupName;
    }

    public void RemoveAllFromGroupAsync(string groupName)
    {
        GroupName = groupName;
    }
}