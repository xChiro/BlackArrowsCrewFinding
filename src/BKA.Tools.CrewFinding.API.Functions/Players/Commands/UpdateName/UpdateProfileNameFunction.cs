using System.Net;
using BKA.Tools.CrewFinding.Players.Commands.Updates;
using BKA.Tools.CrewFinding.Players.Exceptions;

namespace BKA.Tools.CrewFinding.API.Functions.Players.Commands.UpdateName;

public class UpdateProfileNameFunction(IHandleNameUpdater handleNameUpdater, ILoggerFactory loggerFactory) : FunctionBase
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<UpdateProfileNameFunction>();

    [Function("UpdateProfileNameFunction")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "put", Route = "Profiles/Current/Name")] HttpRequestData req)
    {
        try
        {
            var player = await DeserializeBody<UpdateNameRequest>(req);
            await handleNameUpdater.Update(player.UserName);
            
            return OkResponse(req);
        }
        catch (Exception e) when (e is HandlerNameLengthException)
        {
            return NotSuccessResponseAsync(req, HttpStatusCode.BadRequest, e.Message).Result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return InternalServerErrorResponse(req);
        }
    }
}