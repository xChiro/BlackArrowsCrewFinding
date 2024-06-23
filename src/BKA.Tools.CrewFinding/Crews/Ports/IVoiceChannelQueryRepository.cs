using BKA.Tools.CrewFinding.Channels;

namespace BKA.Tools.CrewFinding.Crews.Ports;

public interface IVoiceChannelQueryRepository
{
    public Task<string?> GetVoiceChannelIdByCrewId(string crewId);
    public Task<VoiceChannel[]> GetExpiredChannels(int hoursThreshold);
}
