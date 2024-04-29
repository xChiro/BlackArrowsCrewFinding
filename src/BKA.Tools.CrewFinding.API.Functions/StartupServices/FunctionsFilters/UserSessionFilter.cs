using BKA.Tools.CrewFinding.API.Functions.Authentications;

namespace BKA.Tools.CrewFinding.API.Functions.StartupServices.FunctionsFilters;

public class UserSessionFilter : IUserSessionFilter
{
    private string _userId;
    
    public void Initialize(string userToken)
    {
        var tokenDecoder = new TokenDecoder(userToken);
        _userId = tokenDecoder.GetUserId();
    }

    public string GetUserId()
    {
        return _userId;
    }
}