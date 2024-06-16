using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Queries.PlayerProfiles;

namespace BKA.Tools.CrewFinding.API.Functions.Players.Profiles;

public class CurrentPlayerProfileResponse : IPlayerProfileResponse
{
    public string Id { get; private set; } = string.Empty;
    
    public string CitizenName { get; private set; } = string.Empty;

    public string ActiveCrewName { get; private set; } = string.Empty;
 
    public string ActiveCrewId { get; private set; }  = string.Empty;

    public void SetResponse(Player player)
    {
        Id = player.Id;
        CitizenName = player.CitizenName.Value;
    }

    public void SetResponse(Player player, string activeCrewId, string activeCrewName)
    {
        Id = player.Id;
        CitizenName = player.CitizenName.Value;
        ActiveCrewId = activeCrewId;
        ActiveCrewName = activeCrewName;
    }
}