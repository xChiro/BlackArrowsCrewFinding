using System.Web.Http;
using BKA.Tools.CrewFinding.API.Functions.Authentications;
using BKA.Tools.CrewFinding.Commons.Values.Exceptions;
using BKA.Tools.CrewFinding.Players.Commands.Creation;

namespace BKA.Tools.CrewFinding.API.Functions.Commands.PlayerRegistrations;

public class PlayerSingUpFunction(IPlayerCreator playerCreator, ILoggerFactory loggerFactory) : FunctionBase
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<PlayerSingUpFunction>();

    [Function("PlayerSingUpFunction")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "Players")] HttpRequestData req)
    {
        try
        {
            var tokenDecoder = new TokenDecoder(req);
            var player = await DeserializeBody<PLayerRequest>(req);

            await playerCreator.Create(tokenDecoder.GetUserId(), player.UserName);

            return new OkResult();
        }
        catch (Exception e) when (e is UserIdInvalidException)
        {
            return new NotFoundObjectResult(e.Message);
        }
        catch (Exception e) when (e is CitizenNameEmptyException or CitizenNameLengthException)
        {
            return new BadRequestObjectResult(e.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return new InternalServerErrorResult();
        }
    }
}