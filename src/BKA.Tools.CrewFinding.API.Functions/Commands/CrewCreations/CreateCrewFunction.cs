using System.Net;
using BKA.Tools.CrewFinding.API.Functions.Authentications;
using BKA.Tools.CrewFinding.Commons.Values.Exceptions;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Cultures.Exceptions;
using BKA.Tools.CrewFinding.Players.Exceptions;

namespace BKA.Tools.CrewFinding.API.Functions.Commands.CrewCreations;

public class CreateCrewFunction(ICrewCreator crewCreator, ILoggerFactory loggerFactory) : FunctionBase
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<CreateCrewFunction>();
    
    [Function("CreateCrewFunction")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "Crews")]
        HttpRequestData req)
    {
        try
        {
            var tokenDecoder = new TokenDecoder(req);
            var crewFunctionRequest = await DeserializeBody<CreateCrewFunctionRequest>(req);
            var crewCreatorResponse = await CreateCrew(tokenDecoder, crewFunctionRequest);

            return HandleSuccessfulResponse(req, crewCreatorResponse.CrewId);
        }
        catch (Exception e) when (e is ActivityDescriptionLengthException or ActivityNameLengthException
                                      or PlayerMultipleCrewsException or LanguageNameLengthException)
        {
            return CreateNotSuccessResponse(req, HttpStatusCode.NotFound, e.Message);
        }
        catch (PlayerNotFoundException e)
        {
            return CreateNotSuccessResponse(req, HttpStatusCode.NotFound, e.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return InternalServerErrorResponse(req);
        }
    }

    private async Task<CrewCreatorResponse> CreateCrew(TokenDecoder tokenDecoder,
        CreateCrewFunctionRequest crewFunctionRequest)
    {
        var crewCreatorResponse = new CrewCreatorResponse();
        await crewCreator.Create(crewFunctionRequest.ToCrewCreatorRequest(),
            crewCreatorResponse);
        return crewCreatorResponse;
    }

    private static HttpResponseData HandleSuccessfulResponse(HttpRequestData req, string crewId)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        var result = new {CrewId = crewId};
        response.WriteAsJsonAsync(result);
        
        return response;
    }
}