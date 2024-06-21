using System.Net;
using BKA.Tools.CrewFinding.Channels.Exceptions;
using BKA.Tools.CrewFinding.Channels.invites;
using BKA.Tools.CrewFinding.Commons.Exceptions;

namespace BKA.Tools.CrewFinding.API.Functions.Discord;

public class CreateChannelInviteLink(
    IChannelInviteLinkCreator channelInviteLinkCreator,
    ILoggerFactory loggerFactory) : FunctionBase
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<CreateChannelInviteLink>();

    [Function("CreateDiscordChannelInviteLink")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "Channels/InviteLink")]
        HttpRequestData req,
        string channelId)
    {
        try
        {
            var response = new ChannelInviteLinkCreatorResponse();
            await channelInviteLinkCreator.Create(response);

            return OkResponse(req, response);
        }
        catch (PlayerNotInCrewException)
        {
            return await NotSuccessResponseAsync(req, HttpStatusCode.NotFound, "Player not in crew");
        }
        catch (NotVoiceChannelException)
        {
            return await NotSuccessResponseAsync(req, HttpStatusCode.NotFound, "Voice channel not found");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to create invite link for channel {ChannelId}", channelId);
            return await NotSuccessResponseAsync(req, HttpStatusCode.NotFound, "Channel not found");
        }
    }
}