namespace BKA.Tools.CrewFinding.Crews.Ports;

public interface IVoiceChannelCommandRepository
{
    public Task AddVoiceChannel(string crewId, string voiceChannelId);
}