using BKA.Tools.CrewFinding.Notifications.SignalR.Crews;

namespace BKA.Tools.CrewFinding.Notifications.SignalR;

public interface ISignalRGroupService
{
    void AddUserToGroupAsync(string connectionId, string groupName);
    void SendMessageToGroupAsync<TMessage>(string groupName, TMessage message, string methodName);
    void RemoveUserFromGroupAsync(string connectionId, string groupName);
    void RemoveAllFromGroupAsync(string groupName);
    void SendMessageToUserAsync(string connectionId, string message, string methodName);
}