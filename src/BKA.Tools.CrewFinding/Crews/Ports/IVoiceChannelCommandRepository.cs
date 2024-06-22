namespace BKA.Tools.CrewFinding.Crews.Ports;

public interface IVoiceChannelCommandRepository
{
    public Task AddVoiceChannel(string crewId, string voiceChannelId);
    public Task RemoveChannel(string crewId);
}