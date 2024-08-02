namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks;

public class GroupManagerMock : IGroupManager
{
    private readonly Dictionary<string, List<string>> _groups = new();
    public IReadOnlyDictionary<string, List<string>> Groups => _groups;
    
    public Task AddToGroupAsync(string connectionId, string groupName, CancellationToken cancellationToken = default)
    {
        if (!_groups.ContainsKey(groupName))
        {
            _groups.Add(groupName, new List<string>());
        }

        _groups[groupName].Add(connectionId);

        return Task.CompletedTask;
    }

    public Task RemoveFromGroupAsync(string connectionId, string groupName, CancellationToken cancellationToken = default)
    {
        if (_groups.TryGetValue(groupName, out var group))
        {
            group.Remove(connectionId);
        }

        return Task.CompletedTask;
    }
}