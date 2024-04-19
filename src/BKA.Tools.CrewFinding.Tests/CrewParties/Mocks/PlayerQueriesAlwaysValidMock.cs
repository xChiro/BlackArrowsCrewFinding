using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Tests.CrewParties.Mocks;

public class PlayerQueriesAlwaysValidMock : IPlayerQueries
{
    private readonly string _captainName;
    private readonly bool _playerAlreadyInAParty;

    public PlayerQueriesAlwaysValidMock(string captainName, bool playerAlreadyInAParty = false)
    {
        _captainName = captainName;
        _playerAlreadyInAParty = playerAlreadyInAParty;
    }

    public Task<bool> PlayerAlreadyInACrew(string captainId)
    {
        return Task.FromResult(_playerAlreadyInAParty);
    }

    public Task<Player?> GetPlayer(string playerId)
    {
        return Task.FromResult(Player.Create(playerId, _captainName));
    }
}