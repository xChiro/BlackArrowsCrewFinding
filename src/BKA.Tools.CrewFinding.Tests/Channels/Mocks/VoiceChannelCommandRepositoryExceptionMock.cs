using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Channels.Mocks;

public class VoiceChannelCommandRepositoryExceptionMock<T> : IVoiceChannelCommandRepository where T : Exception, new()
{
    public Task AddVoiceChannel(string crewId, string voiceChannelId)
    {
        throw new T();
    }

    public Task RemoveChannel(string crewId)
    {
        throw new T();
    }
}