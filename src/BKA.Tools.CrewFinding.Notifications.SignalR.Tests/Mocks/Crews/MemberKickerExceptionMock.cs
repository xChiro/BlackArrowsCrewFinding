using BKA.Tools.CrewFinding.Crews.Commands.Kicks;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.Crews;

public class MemberKickerExceptionMock<T> : IMemberKicker where T : Exception, new()
{
    public Task Kick(string memberId, IMemberKickerResponse output)
    {
        throw new T();
    }
}