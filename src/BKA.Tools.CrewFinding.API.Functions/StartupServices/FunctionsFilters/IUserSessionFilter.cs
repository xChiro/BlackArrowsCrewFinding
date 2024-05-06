using BKA.Tools.CrewFinding.Commons.Ports;

namespace BKA.Tools.CrewFinding.API.Functions.StartupServices.FunctionsFilters;

public interface IUserSessionFilter : IUserSession
{
    public void Initialize(string userToken);
    public void Clear();
}