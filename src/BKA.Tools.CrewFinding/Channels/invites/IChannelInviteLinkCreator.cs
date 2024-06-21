namespace BKA.Tools.CrewFinding.Channels.invites;

public interface IChannelInviteLinkCreator
{
    public Task Create(IChannelInviteLinkCreatorResponse output);
}