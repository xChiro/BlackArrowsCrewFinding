using System;
using System.Net;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.CrewParties.Documents;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.Crews;

public class DeletePlayerCrewHistoryTest(
    ICrewCommandRepository crewCommandRepository,
    IDatabaseSettingsProvider<Container> databaseSettingsProvider) : IAsyncLifetime
{
    private Container? _disbandCrewsContainer;
    private string _crewId = string.Empty;

    public Task InitializeAsync()
    {
        _disbandCrewsContainer = databaseSettingsProvider.GetDisbandedCrewsContainer();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Remove_PlayerHistory_When_Is_A_Member_Successfully()
    {
        // Arrange
        var playerId = Guid.NewGuid().ToString();
        var player = Player.Create(playerId, "PlayerName", 2, 16);
        var crew = CrewBuilder.CreateDefaultCrew();
        crew.AddMember(player);
        
        await _disbandCrewsContainer!.CreateItemAsync(CrewDocument.CreateFromCrew(crew), new PartitionKey(crew.Id));
        _crewId = crew.Id;

        // Act
        await crewCommandRepository.DeletePlayerHistory(playerId);

        // Assert
        var crewDocument = await _disbandCrewsContainer!.ReadItemStreamAsync(_crewId, new PartitionKey(_crewId));
        crewDocument.Should().NotBeNull();
        crewDocument.StatusCode.Should().Be(HttpStatusCode.NotFound);
        
        // Cleanup
        _crewId = string.Empty;
    }
    
    [Fact]
    public async Task Remove_PlayerHistory_When_Is_A_Captain_Successfully()
    {
        // Arrange
        var playerId = Guid.NewGuid().ToString();
        var player = Player.Create(playerId, "PlayerName", 2, 16);
        var crew = CrewBuilder.CreateDefaultCrew(captainId: player.Id);
        
        await _disbandCrewsContainer!.CreateItemAsync(CrewDocument.CreateFromCrew(crew), new PartitionKey(crew.Id));
        _crewId = crew.Id;

        // Act
        await crewCommandRepository.DeletePlayerHistory(playerId);

        // Assert
        var crewDocument = await _disbandCrewsContainer!.ReadItemStreamAsync(_crewId, new PartitionKey(_crewId));
        crewDocument.Should().NotBeNull();
        crewDocument.StatusCode.Should().Be(HttpStatusCode.NotFound);
        
        // Cleanup
        _crewId = string.Empty;
    }

    public async Task DisposeAsync()
    {
        if (_crewId == string.Empty)
        {
            return;
        }
        
        await _disbandCrewsContainer!.DeleteItemAsync<CrewDocument>(_crewId, new PartitionKey(_crewId));
    }
}