namespace BKA.Tools.CrewFinding.Players.Queries.PlayerProfiles;

public interface IPlayerProfileViewer
{
    public Task View(string playerId, IPlayerProfileResponse response);
}