using BKA.Tools.CrewFinding.API.Functions;
using BKA.Tools.CrewFinding.API.Functions.StartupServices;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace BKA.Tools.CrewFinding.API.Functions;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var containerBuilder = ContainerBuilderService.CreateContainerBuilder();

        RepositoryService.AddRepositories(builder, containerBuilder);
        ServicesService.AddServices(builder);
    }
}