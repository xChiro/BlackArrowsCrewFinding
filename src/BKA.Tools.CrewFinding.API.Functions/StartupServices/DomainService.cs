using BKA.Tools.CrewFinding.Channels;
using BKA.Tools.CrewFinding.Channels.Expired;
using BKA.Tools.CrewFinding.Channels.invites;
using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;
using BKA.Tools.CrewFinding.Crews.Commands.Disbands;
using BKA.Tools.CrewFinding.Crews.Commands.Expired;
using BKA.Tools.CrewFinding.Crews.Commands.JoinRequests;
using BKA.Tools.CrewFinding.Crews.Commands.Kicks;
using BKA.Tools.CrewFinding.Crews.Commands.Leave;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Crews.Queries.Recent;
using BKA.Tools.CrewFinding.Crews.Queries.Retrievs;
using BKA.Tools.CrewFinding.Players.Commands.Creation;
using BKA.Tools.CrewFinding.Players.Commands.Updates;
using BKA.Tools.CrewFinding.Players.Ports;
using BKA.Tools.CrewFinding.Players.Queries.PlayerProfiles;
using Grpc.Net.Client.Balancer;
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
        var expirationThreshold = Convert.ToInt32(Configuration.GetEnvironmentVariable("recentCrewsHoursThreshold"));
        var minNameLength = Convert.ToInt32(Configuration.GetEnvironmentVariable("minCitizenNameLength"));
        var maxNameLength = Convert.ToInt32(Configuration.GetEnvironmentVariable("maxCitizenNameLength"));

        service.AddScoped<IRecentCrewsRetrieval>(
            serviceProvider =>
                new RecentCrewsRetrieval(
                    serviceProvider.GetRequiredService<ICrewQueryRepository>(), expirationThreshold));

        service.AddScoped<ICrewCreator>(
            serviceProvider =>
            {
                var crewCreator = new CrewCreator(
                    serviceProvider.GetRequiredService<ICrewCommandRepository>(),
                    serviceProvider.GetRequiredService<ICrewValidationRepository>(),
                    serviceProvider.GetRequiredService<IPlayerQueryRepository>(),
                    serviceProvider.GetRequiredService<IUserSession>(), maxCrewSize);

                var voiceChannelCommandRepository =
                    serviceProvider.GetRequiredService<IVoiceChannelHandler>();
                var channelCommandRepository = serviceProvider.GetRequiredService<IVoiceChannelCommandRepository>();
                var domainLogger = serviceProvider.GetRequiredService<IDomainLogger>();

                return new VoicedCrewCreator(crewCreator, voiceChannelCommandRepository, channelCommandRepository,
                    domainLogger);
            });

        service.AddScoped<ICrewDisbandment>(
            serviceProvider =>
            {
                var crewDisbandment = new CrewDisbandment(
                    serviceProvider.GetRequiredService<ICrewQueryRepository>(),
                    serviceProvider.GetRequiredService<ICrewDisbandRepository>(),
                    serviceProvider.GetRequiredService<IUserSession>());

                return new VoicedCrewDisbandment(crewDisbandment,
                    serviceProvider.GetRequiredService<IVoiceChannelHandler>(),
                    serviceProvider.GetRequiredService<IVoiceChannelQueryRepository>(),
                    serviceProvider.GetRequiredService<IDomainLogger>(),
                    serviceProvider.GetRequiredService<IVoiceChannelCommandRepository>());
            });

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

        service.AddScoped<IExpiredCrewRemover>(serviceProvider =>
            new ExpiredCrewRemover(serviceProvider.GetRequiredService<ICrewQueryRepository>(),
                serviceProvider.GetRequiredService<ICrewDisbandRepository>(),
                expirationThreshold));

        service.AddScoped<IMemberKicker>(serviceProvider =>
            new MemberKicker(serviceProvider.GetRequiredService<IUserSession>(),
                serviceProvider.GetRequiredService<ICrewQueryRepository>(),
                serviceProvider.GetRequiredService<ICrewCommandRepository>()));

        service.AddScoped<IHandleNameUpdater>(serviceProvider =>
            new HandleNameUpdater(serviceProvider.GetRequiredService<IPlayerQueryRepository>(),
                serviceProvider.GetRequiredService<IPlayerCommandRepository>(),
                serviceProvider.GetRequiredService<IUserSession>(), maxNameLength, minNameLength));
        
        service.AddScoped<IChannelInviteLinkCreator>(serviceProvider => new ChannelInviteLinkCreator(
            serviceProvider.GetRequiredService<IUserSession>(),
            serviceProvider.GetRequiredService<IVoiceChannelHandler>(),
            serviceProvider.GetRequiredService<IVoiceChannelQueryRepository>(),
            serviceProvider.GetRequiredService<ICrewQueryRepository>()));
        
        service.AddScoped<IExpiredChannelRemover>(serviceProvider => new ExpiredChannelRemover(
            expirationThreshold, 
            serviceProvider.GetRequiredService<IVoiceChannelQueryRepository>(), 
            serviceProvider.GetRequiredService<IVoiceChannelCommandRepository>(),
            serviceProvider.GetRequiredService<IVoiceChannelHandler>()));
    }
}