using System.Net;

namespace BKA.Tools.CrewFinding.API.Functions.SignalR;

public class SignalRFunctions
{     
    [Function("negotiate")]
    public static async Task<HttpResponseData> Negotiate(
        [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequestData req,
        [SignalRConnectionInfoInput(HubName = "CrewHub", UserId = "{headers.x-ms-client-principal-id}")] SignalRConnectionInfo connectionInfo)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(connectionInfo);
        
        return response;
    }
}