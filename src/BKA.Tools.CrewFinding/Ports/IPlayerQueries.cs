using BKA.Tools.CrewFinding.Values;

namespace BKA.Tools.CrewFinding.Ports;

public interface IPlayerQueries
{
    Task<Player?> GetPlayer(string playerId);
}