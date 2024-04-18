using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.Players;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;
using BKA.Tools.CrewFinding.Players;
using FluentAssertions;
using Microsoft.Azure.Cosmos;
using Xunit;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.Players;

public class CreatePlayerTests : IAsyncLifetime
{
    private readonly IDatabaseSettingsProvider<Container> _databaseSettingsProvider;
    private Container? _container;
    private readonly string _playerId;

    public CreatePlayerTests(IDatabaseSettingsProvider<Container> databaseSettingsProvider)
    {
        _playerId = Guid.NewGuid().ToString();
        _databaseSettingsProvider = databaseSettingsProvider;
    }

    public Task InitializeAsync()
    {
        _container = _databaseSettingsProvider.GetPlayerContainer();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Create_A_Player_Successfully()
    {
        // Arrange
        var sut = new PlayerCommands(_container!);
        var player = Player.Create(_playerId, "Rowan");

        // Act
        await sut.Create(player);

        // Assert
        var playerResponse = await _container!.ReadItemAsync<PlayerDocument>(player.Id, new PartitionKey(player.Id));
        playerResponse.Resource.Should().BeEquivalentTo(player);
    }

    public Task DisposeAsync()
    {
        if (_playerId == String.Empty)
            return Task.CompletedTask;

        return _container!.DeleteItemAsync<PlayerDocument>(_playerId, new PartitionKey(_playerId));
    }
}