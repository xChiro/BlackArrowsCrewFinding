namespace BKA.Tools.CrewFinding.Crews.Ports;

public interface IVoicedCrewCommandRepository
{
    public Task SetVoiceChannel(string crewId, string voiceChannelId);
}