using BKA.Tools.CrewFinding.Channels.invites;

namespace BKA.Tools.CrewFinding.API.Functions.Discord;

public class ChannelInviteLinkCreatorResponse : IChannelInviteLinkCreatorResponse
{
    public string Link { get; set; } = string.Empty;

    public void SetResult(string link)
    {
        throw new NotImplementedException();
    }
}