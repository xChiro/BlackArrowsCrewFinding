using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties.Documents;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties;

public class VoicedCrewCommandRepository(Container container) : IVoicedCrewCommandRepository
{
    
    public async Task AddVoiceChannel(string crewId, string voiceChannelId)
    {
        var patchOperations = new List<PatchOperation>
        {
            PatchOperation.Add("/voiceChannelId", voiceChannelId)
        };

        await container.PatchItemAsync<CrewDocument>(crewId, new PartitionKey(crewId), patchOperations);
    }
}