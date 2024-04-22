using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.Players.Documents;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.Azure.DataBase.Repositories.Players;

public class PlayerCommands : IPlayerCommandRepository
{
    private readonly Container _container;

    public PlayerCommands(Container container)
    {
        _container = container;
    }

    public async Task Create(Player player)
    {
        var document = PlayerDocument.CreateFromPlayer(player);
        await _container.CreateItemAsync(document, new PartitionKey(document.Id));
    }
}