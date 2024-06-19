using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.VoicedCrews;

public class VoiceChannelCommandRepositoryExceptionMock<T> : IVoiceChannelCommandRepository where T : Exception, new()
{
    public Task<string> Create(string name)
    {
        throw new T();
    }
}