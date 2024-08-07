using Microsoft.AspNetCore.SignalR;

namespace BKA.Tools.CrewFinding.Notifications.SignalR;

public interface ICrewClient : IClientProxy
{
    Task NotifyPlayerJoined(string groupName, string connectionId);
}