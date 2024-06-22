using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Channels.Expired;

public class ExpireChannelRemover(
    int hoursThreshold,
    IVoiceChannelQueryRepository voiceChannelQueryRepository,
    IVoiceChannelCommandRepository channelCommandRepository,
    IVoiceChannelHandler channelHandlerMock) : IExpireChannelRemover
{
    public async Task Remove()
    {
        var expiredChannels = await voiceChannelQueryRepository.GetExpiredChannels(hoursThreshold);

        foreach (var channel in expiredChannels)
        {
            await channelHandlerMock.Delete(channel.VoiceChannelId);
            await channelCommandRepository.RemoveChannel(channel.VoiceChannelId);
        }
    }
}