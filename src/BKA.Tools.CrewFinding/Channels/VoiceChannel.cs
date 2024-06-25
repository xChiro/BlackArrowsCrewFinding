using BKA.Tools.CrewFinding.Channels.Exceptions;

namespace BKA.Tools.CrewFinding.Channels;

public class VoiceChannel(string crewId, string voiceChannelId)
{
    public string VoiceChannelId { get; } = voiceChannelId;

    public string CrewId { get; } = crewId;

    public static VoiceChannel CreateCustom(string crewId, string channelLink, string regexPattern)
    {
        if (!System.Text.RegularExpressions.Regex.IsMatch(channelLink, regexPattern))
        {
            throw new CustomChannelFormatException(channelLink);
        }

        return new VoiceChannel(crewId, channelLink);
    }
}