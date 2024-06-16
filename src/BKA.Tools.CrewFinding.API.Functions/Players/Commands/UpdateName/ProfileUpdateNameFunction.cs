using System.Collections.Generic;
using System.Net;
using BKA.Tools.CrewFinding.Players.Commands.Updates;
using BKA.Tools.CrewFinding.Players.Exceptions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace BKA.Tools.CrewFinding.API.Functions.Players.Commands.UpdateName;

public class ProfileUpdateNameFunction(HandleNameUpdater handleNameUpdater, ILoggerFactory loggerFactory) : FunctionBase
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<ProfileUpdateNameFunction>();

    [Function("ProfileUpdateNameFunction")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req,
        FunctionContext executionContext)
    {
        try
        {
            var player = await DeserializeBody<UpdateNameRequest>(req);
            await handleNameUpdater.Update(player.UserName);
            return OkResponse(req);
        }
        catch (Exception e) when (e is HandlerNameLengthException)
        {
            return NotSuccessResponseAsync(req, HttpStatusCode.BadRequest, e.Message).Result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return InternalServerErrorResponse(req);
        }
    }
}