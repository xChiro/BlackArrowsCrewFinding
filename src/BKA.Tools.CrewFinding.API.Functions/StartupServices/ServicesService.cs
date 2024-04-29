using System;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players.Creation;
using BKA.Tools.CrewFinding.Players.Ports;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace BKA.Tools.CrewFinding.API.Functions.StartupServices;

public class ServicesService
{
    public static void AddServices(IFunctionsHostBuilder builder)
    {
        var maxCrewSize = Convert.ToInt32(Configuration.GetEnvironmentVariable("maxCrewSize"));

        AddCrewServices(builder, maxCrewSize);
        AddPlayerServices(builder);
    }

    private static void AddPlayerServices(IFunctionsHostBuilder builder)
    {
        builder.Services.AddScoped<IPlayerCreator>(
            serviceProvider =>
                new PlayerCreator(
                    serviceProvider.GetRequiredService<IPlayerCommandRepository>(),
                    Convert.ToInt32(Configuration.GetEnvironmentVariable("minCitizenNameLength")),
                    Convert.ToInt32(Configuration.GetEnvironmentVariable("maxCitizenNameLength"))));
    }

    private static void AddCrewServices(IFunctionsHostBuilder builder, int maxCrewSize)
    {
        builder.Services.AddScoped<ICrewCreator>(
            serviceProvider =>
                new CrewCreator(
                    serviceProvider.GetRequiredService<ICrewCommandRepository>(),
                    serviceProvider.GetRequiredService<ICrewValidationRepository>(),
                    serviceProvider.GetRequiredService<IPlayerQueryRepository>(),
                    maxCrewSize));
    }
}