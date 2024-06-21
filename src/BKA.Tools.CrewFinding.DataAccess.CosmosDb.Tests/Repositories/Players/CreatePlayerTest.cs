using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.Players.Documents;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.Players;

public class CreatePlayerTest(
    IPlayerCommandRepository playerCommandRepository,
    IDatabaseSettingsProvider<Container> databaseSettingsProvider)
    : IAsyncLifetime
{
    private Container? _playerContainer;
    private readonly string _playerId = Guid.NewGuid().ToString();

    public Task InitializeAsync()
    {
        _playerContainer = databaseSettingsProvider.GetPlayerContainer();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Create_A_Player_Successfully()
    {
        // Arrange
        var player = Player.Create(_playerId, "Rowan", 2, 16);

        // Act
        await playerCommandRepository.Create(player);

        // Assert
        var playerResponse = await _playerContainer!.ReadItemAsync<PlayerDocument>(player.Id, new PartitionKey(player.Id));
        playerResponse.Resource.Should().NotBeNull();
        playerResponse.Resource!.Id.Should().Be(player.Id);
        playerResponse.Resource!.CitizenName.Should().Be(player.CitizenName.Value);
    }

    public Task DisposeAsync()
    {
        return _playerId == string.Empty
            ? Task.CompletedTask
            : _playerContainer!.DeleteItemAsync<PlayerDocument>(_playerId, new PartitionKey(_playerId));
    }
}