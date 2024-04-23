using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Azure.DataBase.Repositories.Players;

public class PlayerQueries : IPlayerQueryRepository
{
    private readonly Container _container;

    public PlayerQueries(Container container)
    {
        _container = container;
    }
    
    public Task<bool> PlayerAlreadyInACrew(string captainId)
    {
        throw new NotImplementedException();
    }

    public Task<Player?> GetPlayer(string playerId)
    {
        throw new NotImplementedException();
    }
}