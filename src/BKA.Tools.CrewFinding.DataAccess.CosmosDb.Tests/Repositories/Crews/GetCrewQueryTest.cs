using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties.Documents;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.Crews;

public class GetCrewQueryTest(
    ICrewQueryRepository crewQueryRepository,
    IDatabaseSettingsProvider<Container> databaseSettingsProvider)
    : IAsyncLifetime
{
    private Container? _container;
    private Crew? _crewDocument;

    public Task InitializeAsync()
    {
        _container = databaseSettingsProvider.GetCrewContainer();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Get_Crew_Successfully()
    {
        // Arrange
        _crewDocument = CrewBuilder.CreateDefaultCrew();
        await CreateDocument(_crewDocument);

        // Act
        var crew = await crewQueryRepository.GetCrew(_crewDocument.Id);

        // Assert
        crew.Should().NotBeNull();
        crew!.Id.Should().Be(_crewDocument.Id);
    }
    
    [Fact]
    public async Task Attempt_To_Get_A_Crew_With_Not_Existing_id()
    {
        // Arrange
        const string notExistingId = "notExistingId";

        // Act
        var crew = await crewQueryRepository.GetCrew(notExistingId);

        // Assert
        crew.Should().BeNull();
    }

    private async Task CreateDocument(Crew crew)
    {
        var crewDocument = CrewDocument.CreateFromCrew(crew);

        await _container!.CreateItemAsync(crewDocument, new PartitionKey(crewDocument.Id));
    }

    public Task DisposeAsync()
    {
        return _crewDocument == null
            ? Task.CompletedTask
            : _container!.DeleteItemAsync<CrewDocument>(_crewDocument.Id, new PartitionKey(_crewDocument.Id));
    }
}