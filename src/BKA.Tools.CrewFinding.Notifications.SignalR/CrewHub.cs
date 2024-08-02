using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace BKA.Tools.CrewFinding.Notifications.SignalR;

public class CrewHub : Hub
{
    public static readonly ConcurrentDictionary<string, string?> ConnectionMap = new();

    public override async Task OnConnectedAsync()
    {
        ConnectionMap[Context.ConnectionId] = Context.UserIdentifier;
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        ConnectionMap.TryRemove(Context.ConnectionId, out _);
        await base.OnDisconnectedAsync(exception);
    }
}