using System.Collections;
using System.Collections.ObjectModel;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.Crews
{
    public class Members : IEnumerable<Player>
{
    private readonly List<Player> _players;
    public ReadOnlyCollection<Player> Players => _players.AsReadOnly();
    
    public int MaxAllowed { get; }

    private Members(List<Player> players, int maxAllowed)
    {
        _players = players;
        MaxAllowed = maxAllowed;
    }

    public bool IsAtCapacity()
    {
        return _players.Count == MaxAllowed;
    }

    public void AddMember(Player player)
    {
        if (!IsAtCapacity())
        {
            _players.Add(player);
        }
        else
        {
            throw new Exception("Cannot add more members. Capacity is full.");
        }
    }

    public static Members Create(List<Player> players, int maxAllowed)
    {
        return new Members(players, maxAllowed);
    }

    public static Members CreateSingle(Player player, int maxAllowed)
    {
        var members = new List<Player>()
        {
            player
        };
        
        return new Members(members, maxAllowed);
    }

    public static Members CreateEmpty(int maxAllowed)
    {
        return new Members(new List<Player>(), maxAllowed);
    }

    public IEnumerator<Player> GetEnumerator()
    {
        return Players.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
}