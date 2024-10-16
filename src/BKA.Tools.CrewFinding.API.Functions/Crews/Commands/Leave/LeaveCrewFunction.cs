using System.Net;
using BKA.Tools.CrewFinding.Commons.Exceptions;
using BKA.Tools.CrewFinding.Crews.Commands.Leave;
using BKA.Tools.CrewFinding.Crews.Exceptions;

namespace BKA.Tools.CrewFinding.API.Functions.Crews.Commands.Leave;

public class LeaveCrewFunction(ICrewLeaver crewLeaver, ILoggerFactory loggerFactory) : FunctionBase
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<LeaveCrewFunction>();

    [Function("LeaveCrewFunction")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "Crews/Leave")]
        HttpRequestData req, string id)
    {
        try
        {
            var leaveCrewFunctionResponse = new LeaveCrewFunctionResponse();
            await crewLeaver.Leave(leaveCrewFunctionResponse);
            return OkResponse(req, leaveCrewFunctionResponse);
        }
        catch (Exception e) when (e is CrewNotFoundException or PlayerNotInCrewException)
        {
            return await NotSuccessResponseAsync(req, HttpStatusCode.NotFound, e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return InternalServerErrorResponse(req);
        }
    }
}