using BKA.Tools.CrewFinding.Players.Commands.Removes;

namespace BKA.Tools.CrewFinding.API.Functions.Players.Commands.RemoveAccount;

public class RemoveAccountFunction(IAccountRemover accountRemover, ILoggerFactory loggerFactory) : FunctionBase
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<RemoveAccountFunction>();

    [Function("RemoveAccountFunction")]
    public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "Profiles/Current")] HttpRequestData req,
        FunctionContext executionContext)
    {
        try
        {
            accountRemover.Remove();
            return OkResponse(req);   
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return InternalServerErrorResponse(req);
        }
    }
}