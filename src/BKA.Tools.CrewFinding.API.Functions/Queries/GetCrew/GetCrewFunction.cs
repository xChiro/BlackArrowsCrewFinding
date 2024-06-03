using System.Collections.Generic;
using System.Net;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Tests.Crews.Queries.Recent;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace BKA.Tools.CrewFinding.API.Functions.Queries.GetCrew;

public class GetCrewFunction(ILoggerFactory loggerFactory, IActiveCrewRetrieval activeCrewRetrieval) : FunctionBase
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<GetCrewFunction>();

    [Function("GetCrewFunction")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Crews/{id}")] HttpRequestData req, 
        string id, FunctionContext executionContext)
    {
        try
        {
            var response = new GetCrewResponse();
            await activeCrewRetrieval.Retrieve(id, response);

            return OkResponse(req, response);
        }
        catch (CrewNotFoundException e)
        {
            return await NotSuccessResponseAsync(req, HttpStatusCode.NotFound, e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return InternalServerErrorResponse(req);
        }
    }
}