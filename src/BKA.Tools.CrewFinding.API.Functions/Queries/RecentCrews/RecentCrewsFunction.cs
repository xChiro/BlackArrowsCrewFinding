using BKA.Tools.CrewFinding.Crews.Queries.Recents;

namespace BKA.Tools.CrewFinding.API.Functions.Queries.RecentCrews;

public class RecentCrewsFunction(IRecentCrewsRetrieval recentCrewsRetrieval, ILoggerFactory loggerFactory)
    : FunctionBase
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<RecentCrewsFunction>();

    [Function("RecentCrewsFunction")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Crews/Recent")] HttpRequestData req,
        FunctionContext executionContext)
    {
        try
        {
            var recentCrewsFunctionResponse = new RecentCrewsFunctionResponse();
            await recentCrewsRetrieval.Retrieve(recentCrewsFunctionResponse);

            return OkResponse(req, recentCrewsFunctionResponse);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return InternalServerErrorResponse(req);
        }
    }
}