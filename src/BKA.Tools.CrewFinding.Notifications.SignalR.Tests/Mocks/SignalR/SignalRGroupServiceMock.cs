namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.SignalR;

public class SignalRGroupServiceMock : ISignalRGroupService
{
    public string[] ExcludedUserIds { get; private set; } = [];
    public string UserName { get; private set; } = string.Empty;
    public string RemovedGroupName { get; private set; } = string.Empty;
    public string RemovedUserId { get; private set; } = string.Empty;
    public string UserId { get; private set; } = string.Empty;
    public string GroupName { get; private set; } = string.Empty;
    public object? Message { get; private set; }
    public int RemoveUserFromGroupCalls { get; private set; }
    public int SendMessageToUserAsyncCalls { get; private set; }
    public int SendMessageToGroupAsyncCalls { get; private set; }

    public void AddUserToGroupAsync(string userId, string groupName)
    {
        UserId = userId;
        GroupName = groupName;
    }

    public void SendMessageToGroupAsync<TMessage>(string groupName, TMessage message, string methodName, string[]? exceptUserIds = null)    {
        GroupName = groupName;
        Message = message;
        ExcludedUserIds = exceptUserIds ?? [];
        SendMessageToGroupAsyncCalls++;
    }

    public void RemoveUserFromGroupAsync(string userId, string groupName)
    {
        RemovedUserId = userId;
        GroupName = groupName;
        RemoveUserFromGroupCalls++;
    }

    public void RemoveAllFromGroupAsync(string groupName)
    {
        RemovedGroupName = groupName;
    }


    public void SendMessageToUserAsync<TMessage>(string userId, TMessage message, string methodName)
    {
        UserId = userId;
        Message = message;
        SendMessageToUserAsyncCalls++;
    }
}