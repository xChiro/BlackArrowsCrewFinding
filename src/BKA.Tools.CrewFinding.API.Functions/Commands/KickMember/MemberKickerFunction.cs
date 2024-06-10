using System.Collections.Generic;
using System.Net;
using BKA.Tools.CrewFinding.Crews.Commands.Kicks;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace BKA.Tools.CrewFinding.API.Functions.Commands.KickMember;

public class MemberKickerFunction(IMemberKicker memberKicker, ILoggerFactory loggerFactory) : FunctionBase
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<MemberKickerFunction>();

    [Function("MemberKickerFunction")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "Crews/Members/{id}")] HttpRequestData req, string id,
        FunctionContext executionContext)
    {
        try
        {
            await memberKicker.Kick(id);
            return OkResponse(req);
        }
        catch (NotCaptainException)
        {
            return  await NotSuccessResponseAsync(req, HttpStatusCode.Forbidden, "Only the captain can kick members.");
        }
        catch (NoCrewMemberException)
        {
            return await NotSuccessResponseAsync(req, HttpStatusCode.NotFound, "The member is not in the crew.");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return InternalServerErrorResponse(req);
        }
        
    }
}