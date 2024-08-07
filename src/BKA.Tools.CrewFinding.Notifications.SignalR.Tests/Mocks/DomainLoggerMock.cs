using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks;

internal class DomainLoggerMock : IDomainLogger
{
    public Exception? LastException { get; private set; }
    public string? LastMessage { get; private set; }
    
    public void Log(Exception e, string message)
    {
        LastException = e;
        LastMessage = message;
    }
}