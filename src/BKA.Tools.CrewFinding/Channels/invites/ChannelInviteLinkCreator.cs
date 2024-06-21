using BKA.Tools.CrewFinding.Channels.Exceptions;
using BKA.Tools.CrewFinding.Commons.Exceptions;
using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Channels.invites;

public class ChannelInviteLinkCreator(
    IUserSession userSession,
    IVoiceChannelHandler channelHandlerMock,
    IVoiceChannelQueryRepository voiceChannelQueryRepository,
    ICrewQueryRepository crewQueryRepositoryMock) : IChannelInviteLinkCreator
{
    public async Task Create(IChannelInviteLinkCreatorResponse output)
    {
        var crew = await GetCrewByUserSession();

        if (crew == null)
        {
            throw new PlayerNotInCrewException();
        }

        var voiceChannelId = await voiceChannelQueryRepository.GetVoiceChannelIdByCrewId(crew.Id);

        if (voiceChannelId == null)
        {
            throw new NotVoiceChannelException();
        }

        var link = await channelHandlerMock.CreateInvite(voiceChannelId);

        output.SetResult(link);
    }

    private async Task<Crew?> GetCrewByUserSession()
    {
        var playerId = userSession.GetUserId();
        var crew = await crewQueryRepositoryMock.GetActiveCrewByPlayerId(playerId);
        
        return crew;
    }
}