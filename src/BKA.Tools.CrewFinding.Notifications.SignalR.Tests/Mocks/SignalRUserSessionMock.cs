using BKA.Tools.CrewFinding.Commons.Ports;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks;

public class SignalRUserSessionMock(string connectionId) : ISignalRUserSession
{
    public string GetUserId()
    {
        return Guid.NewGuid().ToString();
    }

    public string GetConnectionId()
    {
        return connectionId;
    }
}