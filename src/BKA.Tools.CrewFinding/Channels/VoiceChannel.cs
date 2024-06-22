namespace BKA.Tools.CrewFinding.Channels;

public class VoiceChannel(string crewId, string voiceChannelId)
{
    public string VoiceChannelId { get; } = voiceChannelId;

    public string CrewId { get; } = crewId;
}