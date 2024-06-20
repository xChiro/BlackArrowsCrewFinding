using System.Collections.Generic;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties.Documents;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;
using BKA.Tools.CrewFinding.Players;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.Crews;

public class UpdateCrewPartMembersTest(
    ICrewCommandRepository crewCommandRepository,
    IDatabaseSettingsProvider<Container> databaseSettingsProvider)
    : IAsyncLifetime
{
    private Container? _container;

    private string _crewId = string.Empty;

    public Task InitializeAsync()
    {
        _container = databaseSettingsProvider.GetCrewContainer();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Update_Crew_Party_Members_Successfully()
    {
        // 
        var crewParty = CreateCrewParty();
        _crewId = crewParty.Id;

        await crewCommandRepository.CreateCrew(crewParty);
        var crewPartyMembers = new List<Player> {Player.Create("25", "John", 2, 16)};

        // Act
        await crewCommandRepository.UpdateMembers(crewParty.Id, crewPartyMembers);

        // Assert
        var crewPartyResponse =
            await _container!.ReadItemAsync<CrewDocument>(crewParty.Id, new PartitionKey(crewParty.Id));
        crewPartyResponse.Resource.Crew[0].Id.Should().Be(crewPartyMembers[0].Id);
        crewPartyResponse.Resource.Crew[0].CitizenName.Should().Be(crewPartyMembers[0].CitizenName.Value);
    }

    private static Crew CreateCrewParty()
    {
        return new Crew(
            Player.Create("24", "Rowan", 2, 16),
            Location.Default(),
            LanguageCollections.Default(),
            PlayerCollection.CreateWithSingle(Player.Create("123412", "Adam", 2, 16), 1),
            Activity.Default());
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return CleanUpCrewParty();
    }

    private async Task CleanUpCrewParty()
    {
        if (_crewId == string.Empty)
            return;

        await _container!.DeleteItemAsync<Crew>(_crewId, new PartitionKey(_crewId));
    }
}