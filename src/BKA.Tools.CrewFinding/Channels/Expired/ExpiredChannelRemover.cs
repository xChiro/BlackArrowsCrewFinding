using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Channels.Expired;

public class ExpiredChannelRemover(
    int hoursThreshold,
    IVoiceChannelQueryRepository voiceChannelQueryRepository,
    IVoiceChannelCommandRepository channelCommandRepository,
    IVoiceChannelHandler channelHandlerMock) : IExpiredChannelRemover
{
    public async Task Remove()
    {
        var expiredChannels = await voiceChannelQueryRepository.GetExpiredChannels(hoursThreshold);

        foreach (var channel in expiredChannels)
        {
            if (!Uri.IsWellFormedUriString(channel.VoiceChannelId, UriKind.Absolute))
                await channelHandlerMock.Delete(channel.VoiceChannelId);

            await channelCommandRepository.RemoveChannel(channel.CrewId);
        }
    }
}