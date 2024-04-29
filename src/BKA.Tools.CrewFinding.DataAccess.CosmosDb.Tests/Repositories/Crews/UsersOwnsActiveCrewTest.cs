using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties.Documents;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.Crews;

public class UsersOwnsActiveCrewTest(ICrewValidationRepository crewValidationRepository,
    IDatabaseSettingsProvider<Container> databaseSettingsProvider):  IAsyncLifetime
{
    private Container? _crewContainer;
    private string _crewIdToBeDeleted = string.Empty;
    
    public Task InitializeAsync()
    {
        _crewContainer = databaseSettingsProvider.GetCrewContainer();
        return Task.CompletedTask;
    }
    
    [Fact]
    public async Task Does_User_Owns_An_Active_Crew_Should_Be_True()
    {
        // Arrange
        var defaultCrew = CrewBuilder.CreateDefaultCrew();
        _crewIdToBeDeleted = defaultCrew.Id;
        await _crewContainer!.CreateItemAsync(CrewDocument.CreateFromCrew(defaultCrew),
            new PartitionKey(defaultCrew.Id));
        
        // Act
        var isAnActiveCrewOwnedBy = await crewValidationRepository.DoesUserOwnAnActiveCrew(defaultCrew.Captain.Id);
        
        // Assert
        isAnActiveCrewOwnedBy.Should().BeTrue();
    }

    public Task DisposeAsync()
    {
        if (_crewContainer is null || string.IsNullOrEmpty(_crewIdToBeDeleted))
            return Task.CompletedTask;

        return _crewContainer.DeleteItemAsync<CrewDocument>(_crewIdToBeDeleted, new PartitionKey(_crewIdToBeDeleted));
    }
}