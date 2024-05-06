using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.API.Functions.Queries.Profiles;

public class UserSessionProfileResponse
{
    public string Id { get; set; } = string.Empty;
    
    public string CitizenName { get; set; } = string.Empty;
    
    public static UserSessionProfileResponse FromPlayer(Player player)
    {
        return new UserSessionProfileResponse
        {
            Id = player.Id,
            CitizenName = player.CitizenName
        };
    }
}