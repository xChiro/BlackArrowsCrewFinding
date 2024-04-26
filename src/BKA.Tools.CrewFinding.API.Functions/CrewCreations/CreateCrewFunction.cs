using System;
using System.Threading.Tasks;
using System.Web.Http;
using BKA.Tools.CrewFinding.API.Functions.Authentications;
using BKA.Tools.CrewFinding.API.Functions.Models;
using BKA.Tools.CrewFinding.Commons.Values.Exceptions;
using BKA.Tools.CrewFinding.Crews.Commands.CreateRequests;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Cultures.Exceptions;
using BKA.Tools.CrewFinding.Players.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace BKA.Tools.CrewFinding.API.Functions.CrewCreations;

public class CreateCrewFunction : FunctionBase
{
    private readonly ICrewCreator _crewCreator;

    public CreateCrewFunction(ICrewCreator crewCreator)
    {
        _crewCreator = crewCreator;
    }

    [FunctionName("CreateCrewFunction")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "Crews")]
        HttpRequest req, ILogger log)
    {
        try
        {
            var tokenDecoder = new TokenDecoder(req);
            var crewFunctionRequest = await DeserializeBody<CreateCrewFunctionRequest>(req);

            var crewCreatorResponse = new CrewCreatorResponse();

            await _crewCreator.Create(crewFunctionRequest.ToCrewCreatorRequest(tokenDecoder.GetUserId()), crewCreatorResponse);

            return new OkObjectResult(new
            {
                crewCreatorResponse.CrewId
            });
        } 
        catch (Exception e) when (e is ActivityDescriptionLengthException or ActivityNameLengthException
                                      or PlayerMultipleCrewsException or LanguageNameLengthException)
        {
            return new BadRequestObjectResult(new ErrorMessageResponse(e.Message));
        }
        catch(PlayerNotFoundException e)
        {
            return new NotFoundObjectResult(new ErrorMessageResponse(e.Message));
        }
        catch (Exception ex)
        {
            log.LogError(ex.Message, ex);
            return new InternalServerErrorResult();
        }
    }
}