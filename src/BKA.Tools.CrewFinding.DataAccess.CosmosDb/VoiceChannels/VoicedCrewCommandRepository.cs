using BKA.Tools.CrewFinding.Azure.DataBase.VoiceChannels.Documents;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Azure.DataBase.VoiceChannels;

public class VoicedCrewCommandRepository(Container container) : IVoicedCrewCommandRepository
{
    
    public async Task AddVoiceChannel(string crewId, string voiceChannelId)
    {
        var voiceChannelDocument = new VoiceChannelDocument
        {
            Id = voiceChannelId,
            CrewId = crewId,
            CreateAt = DateTime.UtcNow
        };

        await container.CreateItemAsync(voiceChannelDocument, new PartitionKey(voiceChannelId));
    }
}