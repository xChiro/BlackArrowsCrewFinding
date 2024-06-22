using System.Linq;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Channels;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.VoicedCrews;

public class VoiceChannelQueryRepositoryMock(VoiceChannel[] voiceChannelId) : IVoiceChannelQueryRepository
{
    public bool GetExpiredChannelIdsCalled { get; private set; }

    public Task<string?> GetVoiceChannelIdByCrewId(string crewId)
    {
        return Task.FromResult(voiceChannelId.First().VoiceChannelId)!;
    }

    public Task<VoiceChannel[]> GetExpiredChannels(int hoursThreshold)
    {
        GetExpiredChannelIdsCalled = true;
        return Task.FromResult(voiceChannelId);
    }

}