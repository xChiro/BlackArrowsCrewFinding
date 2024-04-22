using System.Collections;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.Crews;

public class Members : IEnumerable<Player>
{
    private readonly List<Player> _players;

    public int MaxAllowed { get; }

    private Members(List<Player> players, int maxAllowed)
    {
        _players = players;
        MaxAllowed = maxAllowed;
    }
    
    public static Members Create(IEnumerable<Player> players, int maxAllowed)
    {
        return new Members(players.ToList(), maxAllowed);
    }

    public static Members CreateSingle(Player player, int maxAllowed)
    {
        var members = new List<Player>
        {
            player
        };

        return new Members(members, maxAllowed);
    }

    public static Members CreateEmpty(int maxAllowed)
    {
        return new Members(new List<Player>(), maxAllowed);
    }

    public void AddMember(Player player)
    {
        if (IsAtCapacity())
            throw new CrewFullException();

        _players.Add(player);
    }

    private bool IsAtCapacity()
    {
        return _players.Count == MaxAllowed;
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