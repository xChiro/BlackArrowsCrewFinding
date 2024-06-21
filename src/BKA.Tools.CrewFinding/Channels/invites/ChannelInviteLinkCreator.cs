using BKA.Tools.CrewFinding.Channels.Exceptions;
using BKA.Tools.CrewFinding.Commons.Exceptions;
using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Channels.invites;

public class ChannelInviteLinkCreator(
    IUserSession userSession,
    IVoiceChannelHandler channelHandler,
    IVoiceChannelQueryRepository voiceChannelQueryRepository,
    ICrewQueryRepository crewQueryRepository) : IChannelInviteLinkCreator
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

        var link = await channelHandler.CreateInvite(voiceChannelId);

        output.SetResult(link);
    }

    private async Task<Crew?> GetCrewByUserSession()
    {
        var playerId = userSession.GetUserId();
        var crew = await crewQueryRepository.GetActiveCrewByPlayerId(playerId);
        
        return crew;
    }
}