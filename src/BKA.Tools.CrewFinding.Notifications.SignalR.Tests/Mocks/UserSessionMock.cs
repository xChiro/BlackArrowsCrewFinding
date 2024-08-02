using BKA.Tools.CrewFinding.Commons.Ports;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks;

public record UserSessionMock(string id) : IUserSession
{
    public string GetUserId()
    {
        return id;
    }
}