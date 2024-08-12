using BKA.Tools.CrewFinding.Crews.Commands.Leave;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.SignalR;

public class CrewLeaverExceptionMock<TException> : ICrewLeaver where TException : Exception, new()
{
    public Task Leave(ICrewLeaverResponse output)
    {
        throw new TException();
    }
}