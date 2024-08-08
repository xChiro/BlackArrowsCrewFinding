using BKA.Tools.CrewFinding.Commons.Ports;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks;

public class UserSessionMock(string userId) : IUserSession
{
    public string GetUserId()
    {
        return userId;
    }
}