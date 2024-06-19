using BKA.Tools.CrewFinding.Discord.Models;

namespace BKA.Tools.CrewFinding.Discord;

public interface IDiscordHttpClient
{
    public Task<ChannelCreatorResponse> PostCreateChannelAsync(ChannelCreatorRequest request);
}