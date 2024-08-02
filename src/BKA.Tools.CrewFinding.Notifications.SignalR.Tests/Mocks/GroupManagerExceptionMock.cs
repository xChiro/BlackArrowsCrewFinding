namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests;

public class GroupManagerExceptionMock<TException> : IGroupManager where TException : Exception, new()
{
    public Task AddToGroupAsync(string connectionId, string groupName,
        CancellationToken cancellationToken = new())
    {
        throw new TException();
    }

    public Task RemoveFromGroupAsync(string connectionId, string groupName,
        CancellationToken cancellationToken = new())
    {
        throw new TException();
            
    }
}