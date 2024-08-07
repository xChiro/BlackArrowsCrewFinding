namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.SignalR;

public class SignalRGroupServiceMock : ISignalRGroupService
{
    public string RemovedConnectionId { get; private set; } = string.Empty;
    public string AddedConnectionId { get; private set; } = string.Empty;
    public string GroupName { get; private set; } = string.Empty;
    public object? Message { get; private set; }
    public int RemoveUserFromGroupCalls { get; private set; }

    public void AddUserToGroupAsync(string connectionId, string groupName)
    {
        AddedConnectionId = connectionId;
        GroupName = groupName;
    }

    public void SendMessageToGroupAsync<T>(string groupName, T message, string methodName)
    {
        GroupName = groupName;
        Message = message;
    }

    public void RemoveUserFromGroupAsync(string connectionId, string groupName)
    {
        RemovedConnectionId = connectionId;
        GroupName = groupName;
        RemoveUserFromGroupCalls++;
    }
}