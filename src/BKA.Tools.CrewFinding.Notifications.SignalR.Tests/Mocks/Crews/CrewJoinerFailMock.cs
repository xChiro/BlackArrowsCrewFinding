using BKA.Tools.CrewFinding.Crews.Commands.JoinRequests;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.Crews;

public class CrewJoinerFailMock<TException> : ICrewJoiner where TException : Exception, new()
{
    public Task Join(string crewId)
    {
        throw new TException();
    }
}