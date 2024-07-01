using System.Text.Json;
using BKA.Tools.CrewFinding.Azure.DataBase.Players.Documents;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Azure.DataBase.Players;

public class PlayerQueries(Container container, int minNameLength, int maxNameLength) : IPlayerQueryRepository
{
    public async Task<Player?> GetPlayer(string playerId)
    {
        var response = await container.ReadItemStreamAsync(playerId, new PartitionKey(playerId));

        if (!response.IsSuccessStatusCode) return null;
        
        var document = await new StreamReader(response.Content).ReadToEndAsync();
        
        return JsonSerializer.Deserialize<PlayerDocument>(document, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        })?.ToPlayer(minNameLength, maxNameLength);
    }
}