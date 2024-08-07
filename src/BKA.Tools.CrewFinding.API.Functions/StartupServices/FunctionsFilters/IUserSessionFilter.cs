using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Notifications.SignalR;

namespace BKA.Tools.CrewFinding.API.Functions.StartupServices.FunctionsFilters;

public interface IUserSessionFilter : ISignalRUserSession
{
    public void SetToken(string userToken);
    public void Clear();
    void SetConnectionId(string connectionId);
}