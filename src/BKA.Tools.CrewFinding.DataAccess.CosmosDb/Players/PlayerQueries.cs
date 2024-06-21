using BKA.Tools.CrewFinding.Azure.DataBase.Players.Documents;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Azure.DataBase.Players;

public class PlayerQueries(Container container, int minNameLength, int maxNameLength) : IPlayerQueryRepository
{
    public async Task<Player?> GetPlayer(string playerId)
    {
        try
        {
            var playerDocument = await container.ReadItemAsync<PlayerDocument>(playerId, new PartitionKey(playerId));

            return playerDocument.StatusCode == System.Net.HttpStatusCode.NotFound
                ? null
                : playerDocument.Resource.ToPlayer(minNameLength, maxNameLength);
        }
        catch (CosmosException ex) when (ex.StatusCode is System.Net.HttpStatusCode.NotFound
                                             or System.Net.HttpStatusCode.BadRequest)
        {
            return null;
        }
    }
}