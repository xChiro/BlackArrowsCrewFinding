using BKA.Tools.CrewFinding.Azure.DataBase.VoiceChannels.Documents;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Azure.DataBase.VoiceChannels;

public class VoiceChannelCommandRepository(Container container) : IVoiceChannelCommandRepository
{
    
    public async Task AddVoiceChannel(string crewId, string voiceChannelId)
    {
        var voiceChannelDocument = new VoiceChannelDocument
        {
            Id = crewId,
            ChannelId = voiceChannelId,
            CrewId = crewId,
            CreateAt = DateTime.UtcNow
        };

        await container.CreateItemAsync(voiceChannelDocument, new PartitionKey(crewId));
    }

    public Task RemoveChannel(string crewId)
    {
        return container.DeleteItemAsync<VoiceChannelDocument>(crewId, new PartitionKey(crewId));
    }
}