using System.Net;
using System.Text.Json;
using BKA.Tools.CrewFinding.API.Functions.Models;

namespace BKA.Tools.CrewFinding.API.Functions;

public abstract class FunctionBase
{
    protected static async Task<T> DeserializeBody<T>(HttpRequestData req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var crewFunctionRequest = JsonSerializer.Deserialize<T>(requestBody,
            new JsonSerializerOptions(JsonSerializerDefaults.Web));
        
        return crewFunctionRequest!;
    }
    
    protected static HttpResponseData CreateSuccessfulResponse(HttpRequestData req)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        return response;
    }
    
    protected static HttpResponseData CreateNotSuccessResponse(HttpRequestData req, HttpStatusCode statusCode, string message)
    {
        var response = req.CreateResponse(statusCode);
        response.WriteAsJsonAsync(new ErrorMessageResponse(message));
        
        return response;
    }
    
    protected static HttpResponseData InternalServerErrorResponse(HttpRequestData req)
    {
        return req.CreateResponse(HttpStatusCode.InternalServerError);
    }
}