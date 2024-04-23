namespace BKA.Tools.CrewFinding.Players.Ports;

public interface IPlayerQueryRepository
{
    public Task<Player?> GetPlayer(string playerId);
}