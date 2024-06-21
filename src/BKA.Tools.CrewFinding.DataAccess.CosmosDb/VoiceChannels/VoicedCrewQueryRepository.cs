using BKA.Tools.CrewFinding.Azure.DataBase.VoiceChannels.Documents;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Azure.DataBase.VoiceChannels;

public class VoiceChannelQueryRepository(Container container) : IVoiceChannelQueryRepository
{
    public async Task<string?> GetVoiceChannelIdByCrewId(string crewId)
    {
        var voiceChannel = await container.ReadItemAsync<VoiceChannelDocument>(crewId, new PartitionKey(crewId));
        return voiceChannel.Resource?.ChannelId;
    }
}