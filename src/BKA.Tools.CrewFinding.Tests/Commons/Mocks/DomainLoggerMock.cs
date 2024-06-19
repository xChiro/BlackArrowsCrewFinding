using System;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Tests.Crews.Commands.Creators.VoiceChannel;

namespace BKA.Tools.CrewFinding.Tests.Commons.Mocks;

internal class DomainLoggerMock : IDomainLogger
{
    public string Message { get; set; }

    public Exception E { get; set; }

    public void Log(Exception e, string message)
    {
        E = e;
        Message = message;
    }
}