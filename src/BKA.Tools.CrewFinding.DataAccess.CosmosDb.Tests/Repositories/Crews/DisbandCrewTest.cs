using System.Net;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties.Documents;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.Crews;

public class DisbandCrewTest(
    ICrewDisbandRepository crewDisbandRepository,
    IDatabaseSettingsProvider<Container> databaseSettingsProvider) : IAsyncLifetime
{
    private Container? _disbandedCrewsContainer;
    private Container? _crewContainer;
    private string _crewIdToCleanup = string.Empty;

    public Task InitializeAsync()
    {
        _disbandedCrewsContainer = databaseSettingsProvider.GetDisbandedCrewsContainer();
        _crewContainer = databaseSettingsProvider.GetCrewContainer();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Disband_Crew_Successfully()
    {
        // Arrange
        var crew = CrewBuilder.CreateDefaultCrew();
        _crewContainer!.CreateItemAsync(CrewDocument.CreateFromCrew(crew), new PartitionKey(crew.Id)).Wait();
        _crewIdToCleanup = crew.Id;

        // Act
        await crewDisbandRepository.Disband(crew.Id);

        // Assert
        await VerifyCrewNotFoundAsync(crew.Id);
        await VerifyDisbandedCrewAsync(crew.Id);
    }

    private async Task VerifyCrewNotFoundAsync(string id)
    {
        var response = await _crewContainer!.ReadItemStreamAsync(id, new PartitionKey(id));
        if (response.StatusCode != HttpStatusCode.NotFound)
        {
            var document = await _crewContainer.ReadItemAsync<CrewDocument>(id, new PartitionKey(id));
            document.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }

    private async Task VerifyDisbandedCrewAsync(string id)
    {
        var response = await _disbandedCrewsContainer!.ReadItemStreamAsync(id, new PartitionKey(id));
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var document = await _disbandedCrewsContainer.ReadItemAsync<CrewDocument>(id, new PartitionKey(id));
            document.StatusCode.Should().Be(HttpStatusCode.OK);
            document.Resource.Id.Should().Be(id);
        }
    }

    public Task DisposeAsync()
    {
        return _crewIdToCleanup != string.Empty
            ? _disbandedCrewsContainer!.DeleteItemAsync<CrewDocument>(_crewIdToCleanup,
                new PartitionKey(_crewIdToCleanup))
            : Task.CompletedTask;
    }
}