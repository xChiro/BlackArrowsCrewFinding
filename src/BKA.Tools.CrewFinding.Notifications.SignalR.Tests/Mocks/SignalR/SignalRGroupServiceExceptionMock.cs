namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.SignalR;

public class SignalRGroupServiceExceptionMock<T> : ISignalRGroupService where T : Exception, new()
{
    public int RemoveAllFromGroupCalls { get; private set; }
    public int AddConnectionIdToGroupCalls { get; private set; }
    public int SendMessageToGroupCalls { get; private set; }
    public int RemoveUserFromGroupCalls { get; private set; }


    public void AddUserToGroupAsync(string userId, string groupName)
    {
        AddConnectionIdToGroupCalls++;
        throw new T();
    }

    public void SendMessageToGroupAsync<TMessage>(string groupName, TMessage message, string methodName, string[]? exceptUserIds = null)
    {
        SendMessageToGroupCalls++;
        throw new T();
    }

    public void RemoveUserFromGroupAsync(string userId, string groupName)
    {
        RemoveUserFromGroupCalls++;
        throw new T();
    }

    public void RemoveAllFromGroupAsync(string groupName)
    {
        RemoveAllFromGroupCalls++;
        throw new T();
    }

    public void SendMessageToUserAsync<TMessage>(string userId, TMessage message, string methodName)
    {
        throw new T();
    }
}