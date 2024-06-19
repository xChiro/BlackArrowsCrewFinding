namespace BKA.Tools.CrewFinding.Crews.Ports;

public interface IVoicedCrewCommandRepository
{
    public Task AddVoiceChannel(string crewId, string voiceChannelId);
}