using BKA.Tools.CrewFinding.API.Functions.StartupServices;
using BKA.Tools.CrewFinding.API.Functions.StartupServices.FunctionsFilters;
using BKA.Tools.CrewFinding.Commons.Ports;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var containerBuilder = ContainerBuilderService.CreateContainerBuilder();
var userSession = new UserSessionFilter();

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(options =>
        options.UseMiddleware<UserSessionWorkerMiddleware>())
    .ConfigureServices((context, service) =>
    {
        service.AddScoped<IUserSession>(_ => userSession);
        service.AddScoped<IUserSessionFilter>(_ => userSession);
        service.AddHttpContextAccessor();
        RepositoryService.AddRepositories(service, containerBuilder);
        DomainService.AddServices(service);
        service.AddLogging();
    })
    .Build();

host.Run();