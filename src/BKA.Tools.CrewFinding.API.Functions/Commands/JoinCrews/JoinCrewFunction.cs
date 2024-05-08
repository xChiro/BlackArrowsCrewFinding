using System.Net;
using BKA.Tools.CrewFinding.Crews.Commands.JoinRequests;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Players.Exceptions;

namespace BKA.Tools.CrewFinding.API.Functions.Commands.JoinCrews;

public class JoinCrewFunction(ICrewJoiner crewJoiner, ILoggerFactory loggerFactory) : FunctionBase
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<JoinCrewFunction>();

    [Function("JoinCrewFunction")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "Crews/{id}/Members")] HttpRequestData req,
        FunctionContext executionContext, string id)
    {
        try
        {
            await crewJoiner.Join(id);
            return OkResponse(req);
        }
        catch (Exception e) when (e is CrewNotFoundException or PlayerNotFoundException)
        {
            return await NotSuccessResponseAsync(req, HttpStatusCode.NotFound, e.Message);
        }
        catch (Exception e) when (e is PlayerMultipleCrewsException)
        {
            return await NotSuccessResponseAsync(req, HttpStatusCode.Conflict, e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return InternalServerErrorResponse(req);
        }
    }
}