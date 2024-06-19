namespace BKA.Tools.CrewFinding.Crews.Commands.Creators.VoicedCrews;

public interface IVoicedCrewCommandRepository
{
    public Task SetVoiceChannel(string crewId, string voiceChannelId);
}