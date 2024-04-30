using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;
using BKA.Tools.CrewFinding.Crews.Commands.Disbands;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players.Creation;
using BKA.Tools.CrewFinding.Players.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace BKA.Tools.CrewFinding.API.Functions.StartupServices;

public class ServicesService
{
    public static void AddServices(IServiceCollection service)
    {
        var maxCrewSize = Convert.ToInt32(Configuration.GetEnvironmentVariable("maxCrewSize"));

        AddCrewServices(service, maxCrewSize);
        AddPlayerServices(service);
    }

    private static void AddPlayerServices(IServiceCollection service)
    {
        service.AddScoped<IPlayerCreator>(
            serviceProvider =>
                new PlayerCreator(
                    serviceProvider.GetRequiredService<IPlayerCommandRepository>(),
                    Convert.ToInt32(Configuration.GetEnvironmentVariable("minCitizenNameLength")),
                    Convert.ToInt32(Configuration.GetEnvironmentVariable("maxCitizenNameLength"))));
    }

    private static void AddCrewServices(IServiceCollection service, int maxCrewSize)
    {
        service.AddScoped<ICrewCreator>(
            serviceProvider =>
                new CrewCreator(
                    serviceProvider.GetRequiredService<ICrewCommandRepository>(),
                    serviceProvider.GetRequiredService<ICrewValidationRepository>(),
                    serviceProvider.GetRequiredService<IPlayerQueryRepository>(),
                    serviceProvider.GetRequiredService<IUserSession>(), maxCrewSize));

        service.AddScoped<ICrewDisbandment>(
            serviceProvider =>
                new CrewDisbandment(
                    serviceProvider.GetRequiredService<ICrewValidationRepository>(),
                    serviceProvider.GetRequiredService<ICrewDisbandRepository>(),
                    serviceProvider.GetRequiredService<IUserSession>()));
    }
}