using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.Players;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.Players.Documents;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Players.Ports;
using FluentAssertions;
using Microsoft.Azure.Cosmos;
using Xunit;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.Players;

public class CreatePlayerTest : IAsyncLifetime
{
    private readonly IPlayerCommandRepository _sut;
    private readonly IDatabaseSettingsProvider<Container> _databaseSettingsProvider;
    private Container? _playerContainer;
    private readonly string _playerId;

    public CreatePlayerTest(IPlayerCommandRepository playerCommandRepository,
        IDatabaseSettingsProvider<Container> databaseSettingsProvider)
    {
        _playerId = Guid.NewGuid().ToString();
        _sut = playerCommandRepository;
        _databaseSettingsProvider = databaseSettingsProvider;
    }

    public Task InitializeAsync()
    {
        _playerContainer = _databaseSettingsProvider.GetPlayerContainer();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Create_A_Player_Successfully()
    {
        // Arrange
        var player = Player.Create(_playerId, "Rowan");

        // Act
        await _sut.Create(player);

        // Assert
        var playerResponse = await _playerContainer!.ReadItemAsync<PlayerDocument>(player.Id, new PartitionKey(player.Id));
        playerResponse.Resource.Should().NotBeNull();
        playerResponse.Resource!.Id.Should().Be(player.Id);
        playerResponse.Resource!.CitizenName.Should().Be(player.CitizenName);
    }

    public Task DisposeAsync()
    {
        return _playerId == string.Empty
            ? Task.CompletedTask
            : _playerContainer!.DeleteItemAsync<PlayerDocument>(_playerId, new PartitionKey(_playerId));
    }
}