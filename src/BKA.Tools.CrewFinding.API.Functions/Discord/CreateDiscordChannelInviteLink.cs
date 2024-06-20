using System.Net;
using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.API.Functions.Discord;

public class CreateDiscordChannelInviteLink(
    IVoiceChannelCommandRepository voiceChannelCommandRepository,
    IUserSession userSession,
    ILoggerFactory loggerFactory) : FunctionBase
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<CreateDiscordChannelInviteLink>();

    [Function("CreateDiscordChannelInviteLink")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "Discord/Channel/{channelId}/InviteLink")]
        HttpRequestData req,
        string channelId)
    {
        try
        {
            var userId = userSession.GetUserId();
            var code = await voiceChannelCommandRepository.CreateInvite(channelId, userId);

            return OkResponse(req, new
            {
                Code = code
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to create invite link for channel {ChannelId}", channelId);
            return await NotSuccessResponseAsync(req, HttpStatusCode.NotFound, "Channel not found");
        }
    }
}