namespace BKA.Tools.CrewFinding.Players.Queries.PlayerProfiles;

public interface IProfileViewer
{
    public Task View(string playerId, IProfileResponse response);
}