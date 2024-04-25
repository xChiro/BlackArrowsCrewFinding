using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http;
using BKA.Tools.CrewFinding.API.Functions.Models;
using BKA.Tools.CrewFinding.Commons.Values.Exceptions;
using BKA.Tools.CrewFinding.Crews.CreateRequests;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Players.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace BKA.Tools.CrewFinding.API.Functions.Functions.CrewCreations;

public class CreateCrewFunction
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
            const string userId = "1ASD34-344SDF"; // This should be the authenticated user id

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var crewFunctionRequest = JsonSerializer.Deserialize<CreateCrewFunctionRequest>(requestBody);

            var crewCreatorResponse = new CrewCreatorResponse();

            await _crewCreator.Create(crewFunctionRequest.ToCrewCreatorRequest(userId), crewCreatorResponse);

            return new OkObjectResult(new
            {
                crewCreatorResponse.CrewId
            });
        } 
        catch (Exception e) when (e is ActivityDescriptionLengthException or ActivityNameLengthException
                                      or PlayerMultipleCrewsException)
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