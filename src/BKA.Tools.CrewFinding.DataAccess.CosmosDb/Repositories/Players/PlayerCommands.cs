using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.Players.Documents;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Azure.DataBase.Repositories.Players;

public class PlayerCommands(Container container) : IPlayerCommandRepository
{
    public async Task Create(Player player)
    {
        var document = PlayerDocument.CreateFromPlayer(player);
        await container.CreateItemAsync(document, new PartitionKey(document.Id));
    }
}