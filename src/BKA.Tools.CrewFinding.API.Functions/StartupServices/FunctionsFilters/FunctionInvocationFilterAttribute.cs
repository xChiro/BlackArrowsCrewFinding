using Microsoft.Azure.Functions.Worker.Middleware;

namespace BKA.Tools.CrewFinding.API.Functions.StartupServices.FunctionsFilters;

public class UserSessionWorkerMiddleware(IUserSessionFilter userSession) : IFunctionsWorkerMiddleware
{
    public Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var userToken = GetUserToken(context);

        if (!string.IsNullOrEmpty(userToken))
        {
            userSession.Initialize(userToken);
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
}