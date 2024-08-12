using System.Net;
using BKA.Tools.CrewFinding.Crews.Commands.Disbands;
using BKA.Tools.CrewFinding.Crews.Exceptions;

namespace BKA.Tools.CrewFinding.API.Functions.Crews.Commands.Disband;

public class CrewDisbandFunction(ICrewDisbandment crewDisbandment, ILoggerFactory loggerFactory) : FunctionBase
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<CrewDisbandFunction>();

    [Function("CrewDisbandFunction")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "Crews/Disband")]
        HttpRequestData req)
    {
        try
        {
            var crewDisbandmentResponse = new CrewDisbandmentResponse();
            await crewDisbandment.Disband(crewDisbandmentResponse);
            return OkResponse(req, crewDisbandmentResponse);
        }
        catch (Exception e) when (e is CrewDisbandException)
        {
            return await NotSuccessResponseAsync(req, HttpStatusCode.NotFound, e.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return InternalServerErrorResponse(req);
        }
    }
}