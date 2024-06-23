using BKA.Tools.CrewFinding.Azure.DataBase.VoiceChannels.Documents;
using BKA.Tools.CrewFinding.Channels;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Azure.DataBase.VoiceChannels;

public class VoiceChannelQueryRepository(Container container) : IVoiceChannelQueryRepository
{
    public async Task<string?> GetVoiceChannelIdByCrewId(string crewId)
    {
        var voiceChannel = await container.ReadItemAsync<VoiceChannelDocument>(crewId, new PartitionKey(crewId));
        return voiceChannel.Resource?.ChannelId;
    }

    public async Task<VoiceChannel[]> GetExpiredChannels(int hoursThreshold)
    {
        var expiryDate = DateTime.UtcNow.AddHours(-hoursThreshold);
        const string sqlQueryText = "SELECT * FROM c WHERE c.createAt <= @expiryDate";

        var queryDefinition = new QueryDefinition(sqlQueryText).WithParameter("@expiryDate", expiryDate);
        var queryIterator = container.GetItemQueryIterator<VoiceChannelDocument>(queryDefinition);

        return await queryIterator.ReadNextAsync().ContinueWith(task =>
            task.Result.Select(doc => new VoiceChannel(doc.CrewId, doc.ChannelId)).ToArray());
    }
}