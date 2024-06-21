namespace BKA.Tools.CrewFinding.Crews.Ports;

public interface IVoiceChannelQueryRepository
{
    public Task<string?> GetVoiceChannelIdByCrewId(string crewId);
}