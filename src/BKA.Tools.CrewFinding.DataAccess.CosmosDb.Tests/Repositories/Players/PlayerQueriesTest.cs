using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.Players.Documents;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.Players;

public class PlayerQueriesTest : IAsyncLifetime
{
    private readonly IDatabaseSettingsProvider<Container> _databaseSettingsProvider;
    private readonly IPlayerQueryRepository _queryRepository;
    private Container? _playerContainer;
    private readonly string _playerId;

    public PlayerQueriesTest(IPlayerQueryRepository queryRepository,
        IDatabaseSettingsProvider<Container> databaseSettingsProvider)
    {
        _playerId = Guid.NewGuid().ToString();
        _queryRepository = queryRepository;
        _databaseSettingsProvider = databaseSettingsProvider;
    }

    public Task InitializeAsync()
    {
        _playerContainer = _databaseSettingsProvider.GetPlayerContainer();
        return Task.CompletedTask;
    }
    
    [Fact]
    public async Task Get_Player_Successfully()
    {
        // Arrange
        var player = Player.Create(_playerId, "Zyanya");
        await _playerContainer!.CreateItemAsync(PlayerDocument.CreateFromPlayer(player), new PartitionKey(player.Id));

        // Act
        var playerResponse = await _queryRepository.GetPlayer(_playerId);

        // Assert
        playerResponse.Should().NotBeNull();
        playerResponse!.Id.Should().Be(player.Id);
        playerResponse.CitizenName.Should().Be(player.CitizenName);
    }

    public Task DisposeAsync()
    {
        return _playerId == string.Empty
            ? Task.CompletedTask
            : _playerContainer!.DeleteItemAsync<Player>(_playerId, new PartitionKey(_playerId));
    }
}