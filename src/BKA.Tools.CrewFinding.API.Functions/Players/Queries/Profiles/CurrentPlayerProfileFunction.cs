using System.Net;
using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Players.Exceptions;
using BKA.Tools.CrewFinding.Players.Queries.PlayerProfiles;

namespace BKA.Tools.CrewFinding.API.Functions.Players.Queries.Profiles;

public class CurrentPlayerProfileFunction(
    IProfileViewer profileViewer,
    IUserSession userSession,
    ILoggerFactory loggerFactory) : FunctionBase
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<CurrentPlayerProfileFunction>();

    [Function("CurrentPlayerProfileFunction")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Profiles/Current")]
        HttpRequestData req,
        FunctionContext executionContext)
    {
        try
        {
            var response = new CurrentProfileResponse();
            await profileViewer.View(userSession.GetUserId(), response);

            return OkResponse(req, response);
        }
        catch (PlayerNotFoundException e)
        {
            return await NotSuccessResponseAsync(req, HttpStatusCode.NotFound, e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred while getting user session profile.");
            throw;
        }
    }
}