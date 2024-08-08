using BKA.Tools.CrewFinding.Players.Queries.PlayerProfiles;

namespace BKA.Tools.CrewFinding.Notifications.SignalR.Tests.Mocks.Profiles;

public class ProfileViewerExceptionMock<T> : IProfileViewer where T : Exception, new()
{
    public Task View(string playerId, IProfileResponse response)
    {
        throw new T();
    }
}