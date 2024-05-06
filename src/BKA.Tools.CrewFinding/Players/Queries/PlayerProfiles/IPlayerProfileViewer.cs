namespace BKA.Tools.CrewFinding.Players.Queries.PlayerProfiles;

public interface IPlayerProfileViewer
{
    public Task<Player> View(string playerId);
}