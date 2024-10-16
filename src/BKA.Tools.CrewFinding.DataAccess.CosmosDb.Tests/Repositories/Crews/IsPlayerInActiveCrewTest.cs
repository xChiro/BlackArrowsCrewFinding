using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.CrewParties.Documents;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.Crews;

public class IsPlayerInActiveCrewTest(
    ICrewValidationRepository crewValidationRepository,
    IDatabaseSettingsProvider<Container> databaseSettingsProvider)
    : IAsyncLifetime
{
    private Container? _crewContainer;
    private string _crewIdToBeDeleted = string.Empty;

    public Task InitializeAsync()
    {
        _crewContainer = databaseSettingsProvider.GetCrewContainer();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Player_Is_In_Active_Crew()
    {
        // Arrange
        var playerId = Guid.NewGuid().ToString();
        var crew = CrewBuilder.CreateDefaultCrew(4);
        crew.AddMember(Player.Create(playerId, "Allan", 2, 16));

        await _crewContainer!.CreateItemAsync(CrewDocument.CreateFromCrew(crew));
        _crewIdToBeDeleted = crew.Id;

        // Act
        var isPlayerInActiveCrew = await crewValidationRepository.IsPlayerInActiveCrew(playerId);

        // Assert
        isPlayerInActiveCrew.Should().BeTrue();
    }

    [Fact]
    public async Task Player_Is_Captain_In_Active_Crew()
    {
        // Arrange
        var playerId = Guid.NewGuid().ToString();
        var crew = CrewBuilder.CreateDefaultCrew(4, playerId);
        await _crewContainer!.CreateItemAsync(CrewDocument.CreateFromCrew(crew));
        
        _crewIdToBeDeleted = crew.Id;

        // Act
        var isPlayerInActiveCrew = await crewValidationRepository.IsPlayerInActiveCrew(playerId);

        // Assert
        isPlayerInActiveCrew.Should().BeTrue();
    }

    public Task DisposeAsync()
    {
        return !string.IsNullOrEmpty(_crewIdToBeDeleted)
            ? _crewContainer!.DeleteItemAsync<CrewDocument>(_crewIdToBeDeleted, new PartitionKey(_crewIdToBeDeleted))
            : Task.CompletedTask;
    }
}