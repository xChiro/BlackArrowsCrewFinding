using BKA.Tools.CrewFinding.API.Functions.Authentications;

namespace BKA.Tools.CrewFinding.API.Functions.StartupServices.FunctionsFilters;

public class UserSessionFilter : IUserSessionFilter
{
    private string _userId = string.Empty;
    private string _connectionId = string.Empty;

    public void SetToken(string userToken)
    {
        var tokenDecoder = new TokenDecoder(userToken);
        _userId = tokenDecoder.GetUserId();
    }

    public void SetConnectionId(string connectionId)
    {
        _connectionId = connectionId;
    }

    public void Clear()
    {
        _userId = string.Empty;
    }

    public string GetUserId()
    {
        return _userId;
    }
    
    public string GetConnectionId()
    {
        return _connectionId;
    }
}