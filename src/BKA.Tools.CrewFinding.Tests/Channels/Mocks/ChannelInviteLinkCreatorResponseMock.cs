using BKA.Tools.CrewFinding.Channels.invites;

namespace BKA.Tools.CrewFinding.Tests.Channels.Mocks;

public class ChannelInviteLinkCreatorResponseMock : IChannelInviteLinkCreatorResponse
{
    public string Link { get; set; } = string.Empty;

    public void SetResult(string link)
    {
        Link = link;
    }
}