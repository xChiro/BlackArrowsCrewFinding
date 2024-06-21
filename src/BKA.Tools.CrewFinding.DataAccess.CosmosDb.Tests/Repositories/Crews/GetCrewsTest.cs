using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.CrewParties.Documents;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.Crews;

public class GetCrewsTest(
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
    public async Task Get_Crews_Successfully()
    {
        // Arrange
        var from = DateTime.UtcNow.AddDays(-1);
        var to = DateTime.UtcNow.AddMinutes(5);

        var defaultCrew = CrewBuilder.CreateDefaultCrew();
        _crewIdToBeDeleted = defaultCrew.Id;
        await _crewContainer!.CreateItemAsync(CrewDocument.CreateFromCrew(defaultCrew),
            new PartitionKey(defaultCrew.Id));

        // Act
        var crews = await crewQueryRepository.GetCrews(from, to);

        // Assert
        crews.Should().NotBeEmpty();
        crews.Should().ContainSingle(c => c.Id == defaultCrew.Id);
    }

    public Task DisposeAsync()
    {
        if (_crewContainer is null || string.IsNullOrEmpty(_crewIdToBeDeleted))
            return Task.CompletedTask;

        return _crewContainer.DeleteItemAsync<CrewDocument>(_crewIdToBeDeleted, new PartitionKey(_crewIdToBeDeleted));
    }
}