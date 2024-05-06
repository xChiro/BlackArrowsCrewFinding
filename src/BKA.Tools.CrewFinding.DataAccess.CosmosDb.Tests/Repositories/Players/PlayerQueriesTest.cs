using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.Players.Documents;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.Players;

public class PlayerQueriesTest(IPlayerQueryRepository queryRepository,
    IDatabaseSettingsProvider<Container> databaseSettingsProvider) : IAsyncLifetime
{
    private Container? _playerContainer;
    private string _playerToDeleteId = string.Empty;

    public Task InitializeAsync()
    {
        _playerContainer = databaseSettingsProvider.GetPlayerContainer();
        return Task.CompletedTask;
    }
    
    [Fact]
    public async Task Get_Player_Successfully()
    {
        // Arrange
        _playerToDeleteId = Guid.NewGuid().ToString();
        var player = Player.Create(_playerToDeleteId, "Zyanya");
        await _playerContainer!.CreateItemAsync(PlayerDocument.CreateFromPlayer(player), new PartitionKey(player.Id));

        // Act
        var playerResponse = await queryRepository.GetPlayer(_playerToDeleteId);

        // Assert
        playerResponse.Should().NotBeNull();
        playerResponse!.Id.Should().Be(player.Id);
        playerResponse.CitizenName.Should().Be(player.CitizenName);
    }

    [Fact]
    public async Task Attempt_To_Get_Player_With_Empty_Id_Should_Return_Null()
    {
        // Act
        var playerResponse = await queryRepository.GetPlayer( string.Empty);

        // Assert
        playerResponse.Should().BeNull();
    }

    public Task DisposeAsync()
    {
        return _playerToDeleteId == string.Empty
            ? Task.CompletedTask
            : _playerContainer!.DeleteItemAsync<Player>(_playerToDeleteId, new PartitionKey(_playerToDeleteId));
    }
}