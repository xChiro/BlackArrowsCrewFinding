using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BKA.Tools.CrewFinding.API.Functions;

public abstract class FunctionBase
{
    protected static async Task<T> DeserializeBody<T>(HttpRequest req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var crewFunctionRequest = JsonSerializer.Deserialize<T>(requestBody,
            new JsonSerializerOptions(JsonSerializerDefaults.Web));
        
        return crewFunctionRequest;
    }
}