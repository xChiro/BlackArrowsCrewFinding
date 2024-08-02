namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks;

public class HubContextMock<THub>(IGroupManager groups) : IHubContext<THub>
    where THub : Hub
{
    public IHubClients Clients { get; }
    public IGroupManager Groups { get; } = groups;
}