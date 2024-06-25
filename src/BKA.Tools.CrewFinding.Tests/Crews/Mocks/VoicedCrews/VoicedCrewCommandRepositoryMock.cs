using System.Collections.Generic;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Crews.Mocks.VoicedCrews;

public class VoiceChannelCommandRepositoryMock : IVoiceChannelCommandRepository
{
    public string VoiceChannelId { get; private set; } = string.Empty;
    public string CrewId { get; private set; } = string.Empty;
    public List<string> RemovedCrewId { get; } = [];

    public Task AddVoiceChannel(string crewId, string voiceChannelId)
    {
        CrewId = crewId;
        VoiceChannelId = voiceChannelId;
        return Task.CompletedTask;
    }

    public Task AddCustomVoiceChannel(string crewId, string channelLink)
    {
        throw new System.NotImplementedException();
    }

    public Task RemoveChannel(string crewId)
    {
        RemovedCrewId.Add(crewId);
        return Task.CompletedTask;
    }
}