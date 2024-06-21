using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.VoicedCrews;

public class VoiceChannelCommandRepositoryExceptionMock<T> : IVoiceChannelCommandRepository where T : Exception, new()
{
    public int DeletedChannelCallCount { get; private set; }

    public Task<string> Create(string name)
    {
        throw new T();
    }
    
    public Task Delete(string id)
    {
        DeletedChannelCallCount++;
        throw new T();
    }

    public Task<string> CreateInvite(string channelId)
    {
        throw new T();
    }
}