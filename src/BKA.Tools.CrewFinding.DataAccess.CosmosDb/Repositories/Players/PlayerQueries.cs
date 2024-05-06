using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.Players.Documents;
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

    public async Task<Player?> GetPlayer(string playerId)
    {
        try
        {
            var playerDocument = await _container.ReadItemAsync<PlayerDocument>(playerId, new PartitionKey(playerId));

            return playerDocument.StatusCode == System.Net.HttpStatusCode.NotFound
                ? null
                : playerDocument.Resource.ToPlayer();
        }
        catch (CosmosException ex) when (ex.StatusCode is System.Net.HttpStatusCode.NotFound
                                             or System.Net.HttpStatusCode.BadRequest)
        {
            return null;
        }
    }
}