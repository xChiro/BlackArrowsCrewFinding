namespace BKA.Tools.CrewFinding.Notifications.SignalR;

public interface ISignalRGroupService
{
    void AddUserToGroupAsync(string connectionId, string groupName);
    void SendMessageToGroupAsync<TMessage>(string crewId, TMessage message);
}