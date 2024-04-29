using System.Net;
using BKA.Tools.CrewFinding.Crews.Commands.Disbands;
using BKA.Tools.CrewFinding.Crews.Exceptions;

namespace BKA.Tools.CrewFinding.API.Functions.Commands.CrewDisbands;

public class CrewDisbandFunction(ICrewDisbandment crewDisbandment, ILogger<CrewDisbandFunction> log) : FunctionBase
{
    [Function("CrewDisbandFunction")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "Crews/{id}")] HttpRequestData req, string id)
    {
        try
        {
            await crewDisbandment.Disband(id);

            return req.CreateResponse(HttpStatusCode.OK);
        }
        catch (Exception e) when (e is CrewNotFoundException or PlayerNotInCrewException)
        {
            return CreateNotSuccessResponse(req, HttpStatusCode.NotFound, e.Message);
        }
        catch (Exception ex)
        {
            log.LogError(ex.Message, ex);
            return InternalServerErrorResponse(req);
        }
    }
}