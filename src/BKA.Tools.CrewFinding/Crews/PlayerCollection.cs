using System.Collections;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.Crews;

public class PlayerCollection : IEnumerable<Player>
{
    private readonly List<Player> _players;

    public int MaxSize { get; }

    private PlayerCollection(List<Player> players, int maxSize)
    {
        _players = players;
        MaxSize = maxSize;
    }

    public static PlayerCollection Create(IEnumerable<Player> players, int maxAllowed)
    {
        return new PlayerCollection(players.ToList(), maxAllowed);
    }

    public static PlayerCollection CreateWithSingle(Player player, int maxAllowed)
    {
        var members = new List<Player>
        {
            player
        };

        return new PlayerCollection(members, maxAllowed);
    }

    public static PlayerCollection CreateEmpty(int maxAllowed)
    {
        return new PlayerCollection(new List<Player>(), maxAllowed);
    }

    public void Add(Player player)
    {
        if (IsAtCapacity())
            throw new CrewFullException();

        if (_players.Any(p => p.Id == player.Id))
            return;

        _players.Add(player);
    }

    public bool Remove(string playerId)
    {
        var player = _players.FirstOrDefault(p => p.Id == playerId);
        return player != null && _players.Remove(player);
    }

    private bool IsAtCapacity()
    {
        return _players.Count == MaxSize;
    }

    public IEnumerator<Player> GetEnumerator()
    {
        return _players.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}