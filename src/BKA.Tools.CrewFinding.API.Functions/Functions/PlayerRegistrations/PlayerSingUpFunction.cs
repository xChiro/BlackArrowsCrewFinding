using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http;
using BKA.Tools.CrewFinding.API.Functions.Authentications;
using BKA.Tools.CrewFinding.Commons.Values.Exceptions;
using BKA.Tools.CrewFinding.Players.Creation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BKA.Tools.CrewFinding.API.Functions.Functions.PlayerRegistrations;

public class PlayerSingUpFunction
{
    private readonly IPlayerCreator _playerCreator;

    public PlayerSingUpFunction(IPlayerCreator playerCreator)
    {
        _playerCreator = playerCreator;
    }
    
    [FunctionName("PlayerSingUpFunction")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "Players")] HttpRequest req, ILogger log)
    {
        try
        {
            var tokenDecoder = new TokenDecoder(req);
            using var streamReader = new StreamReader(req.Body);
            var bodyText = await streamReader.ReadToEndAsync();
            var player = JsonSerializer.Deserialize<PLayerRequest>(bodyText);
            
            await _playerCreator.Create(tokenDecoder.GetUserId(), player.UserName);
            
            return new OkResult();
        }
        catch(Exception e) when(e is UserIdInvalidException)
        {
            return new NotFoundObjectResult(e.Message);
        }
        catch(Exception e) when(e is CitizenNameEmptyException or CitizenNameLengthException)
        {
            return new BadRequestObjectResult(e.Message);
        }
        catch (Exception ex)
        {
            log.LogError(ex.Message, ex);
            return new InternalServerErrorResult();
        }
    }
}