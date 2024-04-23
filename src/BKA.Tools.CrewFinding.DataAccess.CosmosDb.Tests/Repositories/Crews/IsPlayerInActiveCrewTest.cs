using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties.Documents;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.Crews;

public class IsPlayerInActiveCrewTest : IAsyncLifetime
{
    private readonly ICrewQueryRepository _crewQueryRepository;
    private readonly IDatabaseSettingsProvider<Container> _databaseSettingsProvider;
    private Container? _crewContainer;
    private string _crewIdToBeDeleted = string.Empty;

    public IsPlayerInActiveCrewTest(ICrewQueryRepository crewQueryRepository,
        IDatabaseSettingsProvider<Container> databaseSettingsProvider)
    {
        _crewQueryRepository = crewQueryRepository;
        _databaseSettingsProvider = databaseSettingsProvider;
    }

    public Task InitializeAsync()
    {
        _crewContainer = _databaseSettingsProvider.GetCrewContainer();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Player_Is_In_Active_Crew()
    {
        // Arrange
        const string playerId = "playerId";
        var crew = CrewBuilder.CreateDefaultCrew(4);
        crew.AddMember(Player.Create(playerId, "Allan"));

        await _crewContainer.CreateItemAsync(CrewDocument.CreateFromCrew(crew));
        _crewIdToBeDeleted = crew.Id;

        // Act
        var isPlayerInActiveCrew = await _crewQueryRepository.IsPlayerInActiveCrew(playerId);

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