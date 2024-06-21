using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.CrewParties.Documents;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.Crews;

public class GetCrewByPlayer( 
    ICrewQueryRepository crewQueryRepository,
    IDatabaseSettingsProvider<Container> databaseSettingsProvider) : IAsyncLifetime
{
    private Container? _crewContainer;
    private string _crewIdToBeDeleted = string.Empty;

    public Task InitializeAsync()
    {
        _crewContainer = databaseSettingsProvider.GetCrewContainer();
        return Task.CompletedTask;
    }
    
    [Fact]
    public async void Get_Crew_By_Member_Successfully()
    {
        // Arrange
        var playerId = Guid.NewGuid().ToString();
        var crew = CrewBuilder.CreateDefaultCrew(4);
        crew.AddMember(Player.Create(playerId, "Allan", 2, 16));
        
        await _crewContainer!.CreateItemAsync(CrewDocument.CreateFromCrew(crew));
        _crewIdToBeDeleted = crew.Id;

        // Act
        var crewResponse = await crewQueryRepository.GetActiveCrewByPlayerId(playerId);

        // Assert
        crewResponse.Should().NotBeNull();
        crewResponse!.Id.Should().Be(crew.Id);
        crewResponse.Members.Should().ContainSingle(m => m.Id == playerId);
    }
    
    [Fact]
    public async void Get_Crew_By_Captain_Successfully()
    {
        // Arrange
        var playerId = Guid.NewGuid().ToString();
        var crew = CrewBuilder.CreateDefaultCrew(4, playerId);
        await _crewContainer!.CreateItemAsync(CrewDocument.CreateFromCrew(crew));
        
        _crewIdToBeDeleted = crew.Id;

        // Act
        var crewResponse = await crewQueryRepository.GetActiveCrewByPlayerId(playerId);

        // Assert
        crewResponse.Should().NotBeNull();
        crewResponse!.Id.Should().Be(crew.Id);
        crewResponse.Captain.Id.Should().Be(playerId);
    }
    
    public Task DisposeAsync()
    {
        return !string.IsNullOrEmpty(_crewIdToBeDeleted)
            ? _crewContainer!.DeleteItemAsync<CrewDocument>(_crewIdToBeDeleted, new PartitionKey(_crewIdToBeDeleted))
            : Task.CompletedTask;
    }
    
}