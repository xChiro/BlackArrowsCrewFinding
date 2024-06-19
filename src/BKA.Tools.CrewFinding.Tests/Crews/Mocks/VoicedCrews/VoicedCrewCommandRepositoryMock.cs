using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.VoicedCrews;

public class VoicedCrewCommandRepositoryMock : IVoicedCrewCommandRepository
{
    public string VoiceChannelId { get; private set; } = string.Empty;

    public string CrewId { get; private set; } = string.Empty;

    public Task SetVoiceChannel(string crewId, string voiceChannelId)
    {
        CrewId = crewId;
        VoiceChannelId = voiceChannelId;
        return Task.CompletedTask;
    }
}