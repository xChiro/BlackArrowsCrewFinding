namespace BKA.Tools.CrewFinding.Players.Ports;

public interface IPlayerQueries
{
    public Task<bool> PlayerAlreadyInACrew(string captainId);
    
    public Task<Player?> GetPlayer(string playerId);
}