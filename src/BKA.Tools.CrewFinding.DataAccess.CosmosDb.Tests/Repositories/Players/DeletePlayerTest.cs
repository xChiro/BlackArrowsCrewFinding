using System;
using System.Net;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.Players.Documents;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.Players;

public class DeletePlayerTest(
    IPlayerCommandRepository playerCommandRepository,
    IDatabaseSettingsProvider<Container> databaseSettingsProvider) : IAsyncLifetime
{
    private Container? _playerContainer;
    private string _playerId = string.Empty;

    public Task InitializeAsync()
    {
        _playerContainer = databaseSettingsProvider.GetPlayerContainer();
        return Task.CompletedTask;
    }
        
    [Fact]
    public async Task Delete_A_Player_Successfully()
    {
        // Arrange
        var player = Player.Create(Guid.NewGuid().ToString(), "Rowan", 2, 16);
        await _playerContainer!.CreateItemAsync(PlayerDocument.CreateFromPlayer(player), new PartitionKey(player.Id));
        _playerId = player.Id;

        // Act
        await playerCommandRepository.Delete(_playerId);

        // Assert
        var playerResponse = await _playerContainer!.ReadItemStreamAsync(_playerId, new PartitionKey(_playerId));
        playerResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        
        // Cleanup
        _playerId = string.Empty;
    }

    public Task DisposeAsync()
    {
        return _playerId == string.Empty
            ? Task.CompletedTask
            : _playerContainer!.DeleteItemAsync<PlayerDocument>(_playerId, new PartitionKey(_playerId));
    }
}