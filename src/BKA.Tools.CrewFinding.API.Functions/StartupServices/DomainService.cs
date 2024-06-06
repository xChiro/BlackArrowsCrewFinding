using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;
using BKA.Tools.CrewFinding.Crews.Commands.Disbands;
using BKA.Tools.CrewFinding.Crews.Commands.JoinRequests;
using BKA.Tools.CrewFinding.Crews.Commands.Leave;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Crews.Queries.Recents;
using BKA.Tools.CrewFinding.Crews.Queries.Retrievs;
using BKA.Tools.CrewFinding.Players.Commands.Creation;
using BKA.Tools.CrewFinding.Players.Ports;
using BKA.Tools.CrewFinding.Players.Queries.PlayerProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace BKA.Tools.CrewFinding.API.Functions.StartupServices;

public static class DomainService
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

        service.AddScoped<IPlayerProfileViewer>(
            serviceProvider =>
                new PlayerProfileViewer(
                    serviceProvider.GetRequiredService<IPlayerQueryRepository>(),
                    serviceProvider.GetRequiredService<ICrewQueryRepository>()));
    }

    private static void AddCrewServices(IServiceCollection service, int maxCrewSize)
    {
        service.AddScoped<IRecentCrewsRetrieval>(
            serviceProvider =>
                new RecentCrewsRetrieval(
                    serviceProvider.GetRequiredService<ICrewQueryRepository>(),
                    Convert.ToInt32(Configuration.GetEnvironmentVariable("recentCrewsHoursThreshold"))));

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
                    serviceProvider.GetRequiredService<ICrewQueryRepository>(),
                    serviceProvider.GetRequiredService<ICrewDisbandRepository>(),
                    serviceProvider.GetRequiredService<IUserSession>()));

        service.AddScoped<ICrewJoiner>(
            serviceProvider =>
                new CrewJoiner(
                    serviceProvider.GetRequiredService<ICrewValidationRepository>(),
                    serviceProvider.GetRequiredService<ICrewQueryRepository>(),
                    serviceProvider.GetRequiredService<ICrewCommandRepository>(),
                    serviceProvider.GetRequiredService<IPlayerQueryRepository>(),
                    serviceProvider.GetRequiredService<IUserSession>()));

        service.AddScoped<ICrewLeaver>(serviceProvider =>
            new CrewLeaver(serviceProvider.GetRequiredService<ICrewQueryRepository>(),
                serviceProvider.GetRequiredService<ICrewCommandRepository>(),
                serviceProvider.GetRequiredService<IUserSession>()));

        service.AddScoped<IActiveCrewRetrieval>(serviceProvider =>
            new ActiveCrewRetrieval(serviceProvider.GetRequiredService<ICrewQueryRepository>()));
    }
}