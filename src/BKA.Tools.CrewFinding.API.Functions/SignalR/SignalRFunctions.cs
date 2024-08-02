using System.Net;

namespace BKA.Tools.CrewFinding.API.Functions.SignalR;

public class SignalRFunctions
{     
    [Function("negotiate")]
    public static HttpResponseData Negotiate(
        [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequestData req,
        [SignalRConnectionInfoInput(HubName = "crewHub")] string connectionInfo)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json");
        response.WriteString(connectionInfo);
        
        return response;
    }
}