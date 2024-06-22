using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Channels;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.VoicedCrews;

public class VoiceChannelQueryRepositoryEmptyMock : IVoiceChannelQueryRepository
{
    public int GetExpiredChannelCount { get; private set; }

    public Task<string?> GetVoiceChannelIdByCrewId(string crewId)
    {
        return Task.FromResult((string?)null);
    }

    public Task<VoiceChannel[]> GetExpiredChannels(int hoursThreshold)
    {
        GetExpiredChannelCount++;
        return Task.FromResult(Array.Empty<VoiceChannel>());
    }
}