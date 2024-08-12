namespace BKA.Tools.CrewFinding.Players.Queries.PlayerProfiles;

public interface IProfileResponse
{
    public void SetResponse(Player player);
    public void SetResponse(Player player, string activeCrewId, string activeCrewName);
}