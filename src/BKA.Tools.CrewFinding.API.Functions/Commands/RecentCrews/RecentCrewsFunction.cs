using BKA.Tools.CrewFinding.Crews.Queries.Recents;

namespace BKA.Tools.CrewFinding.API.Functions.Commands.RecentCrews;

public class RecentCrewsFunction(IRecentCrewsRetrieval recentCrewsRetrieval, ILoggerFactory loggerFactory) : FunctionBase
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<RecentCrewsFunction>();

    [Function("RecentCrewsFunction")]
    public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Crews/Recent")] 
        HttpRequestData req, FunctionContext executionContext)
    {
        try
        {
            var recentCrewsFunctionResponse = new RecentCrewsFunctionResponse();
            recentCrewsRetrieval.Retrieve(recentCrewsFunctionResponse);
            
            return OkResponse(req, recentCrewsFunctionResponse);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return InternalServerErrorResponse(req);
        }
    }
}