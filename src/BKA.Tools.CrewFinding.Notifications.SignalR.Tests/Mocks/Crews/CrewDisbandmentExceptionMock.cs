using BKA.Tools.CrewFinding.Crews.Commands.Disbands;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.Crews;

public class CrewDisbandmentExceptionMock<T> : ICrewDisbandment where T : Exception, new()
{
    public Task Disband(ICrewDisbandmentResponse? output = null)
    {
        throw new T();
    }
}