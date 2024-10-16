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
using BKA.Tools.CrewFinding.Notifications.SignalR;
using BKA.Tools.CrewFinding.Notifications.SignalR.Crews;
using BKA.Tools.CrewFinding.Notifications.SignalR.Profiles;
using BKA.Tools.CrewFinding.Players.Commands.Creation;
using BKA.Tools.CrewFinding.Players.Commands.Removes;
using BKA.Tools.CrewFinding.Players.Commands.Updates;
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

        service.AddScoped<IProfileViewer>(
            serviceProvider =>
            {
                var profileViewer = new ProfileViewer(
                    serviceProvider.GetRequiredService<IPlayerQueryRepository>(),
                    serviceProvider.GetRequiredService<ICrewQueryRepository>());

                return new ProfileViewerSignalR(profileViewer,
                    serviceProvider.GetRequiredService<ISignalRGroupService>(),
                    serviceProvider.GetRequiredService<IDomainLogger>());
            });
    }

    private static void AddCrewServices(IServiceCollection service, int maxCrewSize)
    {
        var expirationThreshold = Convert.ToInt32(Configuration.GetEnvironmentVariable("recentCrewsHoursThreshold"));
        var minNameLength = Convert.ToInt32(Configuration.GetEnvironmentVariable("minCitizenNameLength"));
        var maxNameLength = Convert.ToInt32(Configuration.GetEnvironmentVariable("maxCitizenNameLength"));
        const string regexPattern = @"^https:\/\/discord\.gg\/([a-zA-Z0-9]{6,9})$";

        service.AddScoped<IRecentCrewsRetrieval>(
            serviceProvider =>
                new RecentCrewsRetrieval(
                    serviceProvider.GetRequiredService<ICrewQueryRepository>(), expirationThreshold));

        service.AddScoped<ICrewCreator>(
            serviceProvider =>
            {
                var requiredService = serviceProvider.GetRequiredService<IUserSession>();

                var crewCreator = new CrewCreator(
                    serviceProvider.GetRequiredService<ICrewCommandRepository>(),
                    serviceProvider.GetRequiredService<ICrewValidationRepository>(),
                    serviceProvider.GetRequiredService<IPlayerQueryRepository>(),
                    requiredService, maxCrewSize);

                var voiceChannelCommandRepository =
                    serviceProvider.GetRequiredService<IVoiceChannelHandler>();
                var channelCommandRepository = serviceProvider.GetRequiredService<IVoiceChannelCommandRepository>();
                var domainLogger = serviceProvider.GetRequiredService<IDomainLogger>();

                var voicedCrew = new VoicedCrewCreator(crewCreator, voiceChannelCommandRepository,
                    channelCommandRepository,
                    domainLogger, regexPattern);

                var signalRGroupService = serviceProvider.GetRequiredService<ISignalRGroupService>();

                return new CrewCreatorSignalR(voicedCrew,
                    signalRGroupService,
                    requiredService,
                    domainLogger);
            });

        service.AddScoped<ICrewDisbandment>(
            serviceProvider =>
            {
                var requiredService = serviceProvider.GetRequiredService<IUserSession>();

                var crewDisbandment = new CrewDisbandment(
                    serviceProvider.GetRequiredService<ICrewQueryRepository>(),
                    serviceProvider.GetRequiredService<ICrewDisbandRepository>(),
                    requiredService);

                var domainLoggerMock = serviceProvider.GetRequiredService<IDomainLogger>();
                var voicedDisbandment = new VoicedCrewDisbandment(crewDisbandment,
                    serviceProvider.GetRequiredService<IVoiceChannelHandler>(),
                    serviceProvider.GetRequiredService<IVoiceChannelQueryRepository>(),
                    domainLoggerMock,
                    serviceProvider.GetRequiredService<IVoiceChannelCommandRepository>());

                var signalRGroupService = serviceProvider.GetRequiredService<ISignalRGroupService>();

                return new CrewDisbandmentSignalR(voicedDisbandment, signalRGroupService, requiredService,
                    domainLoggerMock);
            });

        service.AddScoped<ICrewJoiner>(
            serviceProvider =>
            {
                var userSession = serviceProvider.GetRequiredService<IUserSession>();

                var playersQueryRepository = serviceProvider.GetRequiredService<IPlayerQueryRepository>();
                var crewJoiner = new CrewJoiner(
                    serviceProvider.GetRequiredService<ICrewValidationRepository>(),
                    serviceProvider.GetRequiredService<ICrewQueryRepository>(),
                    serviceProvider.GetRequiredService<ICrewCommandRepository>(),
                    playersQueryRepository,
                    userSession);

                var requiredService = serviceProvider.GetRequiredService<IDomainLogger>();
                var signalRGroupService = serviceProvider.GetRequiredService<ISignalRGroupService>();

                return new CrewJoinerSignalR(crewJoiner, signalRGroupService,
                    playersQueryRepository, userSession, requiredService);
            });

        service.AddScoped<ICrewLeaver>(serviceProvider =>
        {
            var userSession = serviceProvider.GetRequiredService<IUserSession>();
            var crewLeaver = new CrewLeaver(serviceProvider.GetRequiredService<ICrewQueryRepository>(),
                serviceProvider.GetRequiredService<ICrewCommandRepository>(), userSession);

            var signalRGroupService = serviceProvider.GetRequiredService<ISignalRGroupService>();
            var domainLogger = serviceProvider.GetRequiredService<IDomainLogger>();

            return new CrewLeaverSignalR(crewLeaver, domainLogger, signalRGroupService, userSession);
        });

        service.AddScoped<IActiveCrewRetrieval>(serviceProvider =>
            new ActiveCrewRetrieval(serviceProvider.GetRequiredService<ICrewQueryRepository>()));

        service.AddScoped<IExpiredCrewRemover>(serviceProvider =>
            new ExpiredCrewRemover(serviceProvider.GetRequiredService<ICrewQueryRepository>(),
                serviceProvider.GetRequiredService<ICrewDisbandRepository>(),
                expirationThreshold));

        service.AddScoped<IMemberKicker>(serviceProvider =>
        {
            var memberKicker = new MemberKicker(serviceProvider.GetRequiredService<IUserSession>(),
                serviceProvider.GetRequiredService<ICrewQueryRepository>(),
                serviceProvider.GetRequiredService<ICrewCommandRepository>());

            var signalRGroupService = serviceProvider.GetRequiredService<ISignalRGroupService>();

            return new MemberKickerSignalR(memberKicker, serviceProvider.GetRequiredService<IDomainLogger>(),
                signalRGroupService);
        });

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

        service.AddScoped<IAccountRemover>(serviceProvider => new AccountRemover(
            serviceProvider.GetRequiredService<ICrewQueryRepository>(),
            serviceProvider.GetRequiredService<ICrewCommandRepository>(),
            serviceProvider.GetRequiredService<IPlayerCommandRepository>(),
            serviceProvider.GetRequiredService<IUserSession>()));
    }
}