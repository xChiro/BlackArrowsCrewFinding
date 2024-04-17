namespace BKA.Tools.CrewFinding.Players.Ports;

public interface IPlayerQueries
{
    public Task<Player?> GetPlayer(string playerId);
}