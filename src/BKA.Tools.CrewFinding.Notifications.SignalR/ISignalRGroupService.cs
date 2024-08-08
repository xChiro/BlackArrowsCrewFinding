using BKA.Tools.CrewFinding.Notifications.SignalR.Crews;

namespace BKA.Tools.CrewFinding.Notifications.SignalR;

public interface ISignalRGroupService
{
    void AddUserToGroupAsync(string userId, string groupName);
    void RemoveUserFromGroupAsync(string userId, string groupName);
    void RemoveAllFromGroupAsync(string groupName);
    void SendMessageToGroupAsync<TMessage>(string groupName, TMessage message, string methodName);
    void SendMessageToUserAsync<TMessage>(string userId, TMessage message, string methodName);
}