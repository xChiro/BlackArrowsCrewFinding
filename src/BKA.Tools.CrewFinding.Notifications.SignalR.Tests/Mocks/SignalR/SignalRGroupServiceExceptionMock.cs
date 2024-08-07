namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.SignalR;

public class SignalRGroupServiceExceptionMock<T> : ISignalRGroupService where T : Exception, new()
{
    public int AddConnectionIdToGroupCalls { get; private set; }
    public int SendMessageToGroupCalls { get; private set; }
    
    public void AddUserToGroupAsync(string connectionId, string groupName)
    {
        AddConnectionIdToGroupCalls++;
        throw new T();
    }

    public void SendMessageToGroupAsync<TMessage>(string crewId, TMessage message)
    {
        SendMessageToGroupCalls++;
        throw new T();
    }
}