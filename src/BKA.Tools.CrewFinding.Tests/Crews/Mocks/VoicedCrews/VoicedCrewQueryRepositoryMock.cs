using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.VoicedCrews;

public class VoiceChannelQueryRepositoryMock(string voiceChannelId) : IVoiceChannelQueryRepository
{
    public Task<string?> GetVoiceChannelIdByCrewId(string crewId)
    {
        return Task.FromResult(voiceChannelId)!;
    }
}