using System.Collections.Generic;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.CrewParties;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Values;
using FluentAssertions;
using Microsoft.Azure.Cosmos;
using Xunit;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.CrewParties;

public class UpdateCrewPartMembersTests : IAsyncLifetime
{
    private readonly IDatabaseSettingsProvider<Container> _databaseSettingsProvider;
    private Container? _container;

    private string _crewPartyId = string.Empty;

    public UpdateCrewPartMembersTests(IDatabaseSettingsProvider<Container> databaseSettingsProvider)
    {
        _databaseSettingsProvider = databaseSettingsProvider;
    }

    public Task InitializeAsync()
    {
        _container =_databaseSettingsProvider.GetCrewPartyContainer();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Update_Crew_Party_Members_Successfully()
    {
        // Arrange
        var sut = new CrewCommands(_container!);
        var crewParty = CreateCrewParty();
        _crewPartyId = crewParty.Id;
        
        await sut.CreateCrew(crewParty);
        var crewPartyMembers = new List<Player> {Player.Create("25", "John")};

        // Act
        await sut.UpdateMembers(crewParty.Id, crewPartyMembers);

        // Assert
        var crewPartyResponse =
            await _container!.ReadItemAsync<CrewDocument>(crewParty.Id, new PartitionKey(crewParty.Id));
        crewPartyResponse.Resource.Members.Should().BeEquivalentTo(crewPartyMembers);
    }

    private static Crew CreateCrewParty()
    {
        return new Crew(
            Player.Create("24", "Rowan"),
            new CrewName("Rowan"),
            Location.DefaultLocation(),
            LanguageCollections.Default(),
            Members.CreateSingle(Player.Create("123412", "Adam"), 1),
            Activity.Default());
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return CleanUpCrewParty();
    }

    private async Task CleanUpCrewParty()
    {
        if (_crewPartyId == string.Empty)
            return;

        await _container!.DeleteItemAsync<Crew>(_crewPartyId, new PartitionKey(_crewPartyId));
    }
}