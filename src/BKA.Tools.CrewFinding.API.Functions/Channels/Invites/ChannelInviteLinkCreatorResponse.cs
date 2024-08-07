using BKA.Tools.CrewFinding.Channels.invites;

namespace BKA.Tools.CrewFinding.API.Functions.Channels.Invites;

public class ChannelInviteLinkCreatorResponse : IChannelInviteLinkCreatorResponse
{
    public string Link { get; set; } = string.Empty;

    public void SetResult(string link)
    {
        Link = link;
    }
}