using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Commands.Creators.VoicedCrews;
using BKA.Tools.CrewFinding.Tests.Crews.Commands.Creators.VoiceChannel;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks;

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