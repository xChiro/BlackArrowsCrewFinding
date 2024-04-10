using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Ports;

public interface IPlayerQueries
{
    public Task<Player?> GetPlayer(string playerId);
}