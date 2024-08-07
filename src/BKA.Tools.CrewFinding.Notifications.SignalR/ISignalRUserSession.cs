using BKA.Tools.CrewFinding.Commons.Ports;

namespace BKA.Tools.CrewFinding.Notifications.SignalR;

public interface ISignalRUserSession : IUserSession
{
    public string GetConnectionId();
}