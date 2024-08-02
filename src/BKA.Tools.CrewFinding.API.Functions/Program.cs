using BKA.Tools.CrewFinding.API.Functions;
using BKA.Tools.CrewFinding.API.Functions.Middlewares;
using BKA.Tools.CrewFinding.API.Functions.StartupServices;
using BKA.Tools.CrewFinding.API.Functions.StartupServices.FunctionsFilters;
using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.KeyVault;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var keySecretsProvider = new KeySecretProviderBuilder(Configuration.GetEnvironmentVariable("keyVaultEndpoint")).Build();
var userSession = new UserSessionFilter();

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(options => { options.UseMiddleware<UserSessionWorkerMiddleware>(); })
    .ConfigureServices((_, services) =>
    {
        services.AddHttpContextAccessor();
        services.AddLogging();
        services.AddScoped<IDomainLogger>(sp => new DomainLogger(sp.GetRequiredService<ILogger<DomainLogger>>()));
        services.AddScoped<IUserSession>(_ => userSession);
        services.AddScoped<IUserSessionFilter>(_ => userSession);
        services.AddSignalR();

        InfrastructureServices.AddRepositories(services, keySecretsProvider);
        DomainService.AddServices(services);
    })
    .Build();

host.Run();