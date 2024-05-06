using System.Collections.Generic;
using System.Net;
using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Players.Exceptions;
using BKA.Tools.CrewFinding.Players.Queries.PlayerProfiles;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

namespace BKA.Tools.CrewFinding.API.Functions.Queries.Profiles;

public class UserSessionProfileFunction(
    IPlayerProfileViewer playerProfileViewer,
    IUserSession userSession,
    ILoggerFactory loggerFactory) : FunctionBase
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<UserSessionProfileFunction>();

    [Function("UserSessionProfileFunction")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "User/Profiles")]
        HttpRequestData req,
        FunctionContext executionContext)
    {
        try
        {
            var player = await playerProfileViewer.View(userSession.GetUserId());

            return OkResponse(req, UserSessionProfileResponse.FromPlayer(player));
        }
        catch (PlayerNotFoundException e)
        {
            return NotSuccessResponse(req, HttpStatusCode.NotFound, e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred while getting user session profile.");
            throw;
        }
    }
}