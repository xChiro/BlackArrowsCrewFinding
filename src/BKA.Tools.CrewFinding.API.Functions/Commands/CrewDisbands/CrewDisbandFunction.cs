using System.Net;
using BKA.Tools.CrewFinding.Crews.Commands.Disbands;
using BKA.Tools.CrewFinding.Crews.Exceptions;

namespace BKA.Tools.CrewFinding.API.Functions.Commands.CrewDisbands;

public class CrewDisbandFunction(ICrewDisbandment crewDisbandment, ILoggerFactory loggerFactory) : FunctionBase
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<CrewDisbandFunction>();
    
    [Function("CrewDisbandFunction")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "DELETE", Route = "Crews/Disband")] HttpRequestData req)
    {
        try
        {
            await crewDisbandment.Disband();
            return req.CreateResponse(HttpStatusCode.OK);
        }
        catch (Exception e) when (e is CrewDisbandException)
        {
            return await NotSuccessResponseAsync(req, HttpStatusCode.NotFound, e.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return InternalServerErrorResponse(req);
        }
    }
}