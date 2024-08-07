using BKA.Tools.CrewFinding.API.Functions.StartupServices.FunctionsFilters;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace BKA.Tools.CrewFinding.API.Functions.Middlewares;

public class UserSessionWorkerMiddleware(IUserSessionFilter userSession) : IFunctionsWorkerMiddleware
{
    public Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var userToken = GetUserToken(context);
        var connectionId = GetConnectionId(context);
        
        if (!string.IsNullOrEmpty(connectionId))
        {
            userSession.SetConnectionId(connectionId);
        }
        
        if (!string.IsNullOrEmpty(userToken))
        {
            userSession.SetToken(userToken);
        }
        else
        {
            userSession.Clear();
        }

        return next(context);
    }

    private static string GetUserToken(FunctionContext context)
    {
        if (context.GetHttpRequestData()?.Headers.TryGetValues("Authorization", out var values) == true)
        {
            return values.FirstOrDefault() ?? string.Empty;
        }

        return string.Empty;
    }
    
    private static string GetConnectionId(FunctionContext context)
    {
        if (context.GetHttpRequestData()?.Headers.TryGetValues("ConnectionId", out var values) == true)
        {
            return values.FirstOrDefault() ?? string.Empty;
        }

        return string.Empty;
    }
}