using BKA.Tools.CrewFinding.Commons.Ports;
using BKA.Tools.CrewFinding.Notifications.SignalR;

namespace BKA.Tools.CrewFinding.API.Functions.StartupServices.FunctionsFilters;

public interface IUserSessionFilter : IUserSession
{
    public void SetToken(string userToken);
    public void Clear();
}