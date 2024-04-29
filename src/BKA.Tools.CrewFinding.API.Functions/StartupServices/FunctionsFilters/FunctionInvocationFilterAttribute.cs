using Microsoft.Azure.Functions.Worker.Middleware;

namespace BKA.Tools.CrewFinding.API.Functions.StartupServices.FunctionsFilters;

public class UserSessionWorkerMiddleware(IUserSessionFilter userSession) : IFunctionsWorkerMiddleware
{
    public Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var userToken = context.GetHttpRequestData().Headers.GetValues("Authorization").FirstOrDefault() ??
                        string.Empty;
        userSession.Initialize(userToken);

        return next(context);
    }
}