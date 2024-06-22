using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Channels;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Channels.Mocks;

public class VoiceChannelQueryRepositoryExceptionMock<T> : IVoiceChannelQueryRepository where T : Exception, new()
{
    public Task<string?> GetVoiceChannelIdByCrewId(string crewId)
    {
        throw new T();
    }

    public Task<VoiceChannel[]> GetExpiredChannels(int hoursThreshold)
    {
        throw new T();
    }
}