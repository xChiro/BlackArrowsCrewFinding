using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.API.Functions.Queries.Profiles;

public class CurrentPlayerProfileResponse
{
    public string Id { get; set; } = string.Empty;
    
    public string CitizenName { get; set; } = string.Empty;
    
    public static CurrentPlayerProfileResponse FromPlayer(Player player)
    {
        return new CurrentPlayerProfileResponse
        {
            Id = player.Id,
            CitizenName = player.CitizenName
        };
    }
}