using System.Collections.Generic;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.CrewParties;
using BKA.Tools.CrewFinding.Azure.DataBase.Models;
using BKA.Tools.CrewFinding.CrewParties;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Values;
using FluentAssertions;
using Microsoft.Azure.Cosmos;
using Xunit;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests;

public class UpdateCrewPartMembersTests : IAsyncLifetime
{
    private readonly IDatabaseSettingsProvider<Container> _databaseSettingsProvider;
    private Container? _container;

    private CrewParty? _crewParty;

    public UpdateCrewPartMembersTests(IDatabaseSettingsProvider<Container> databaseSettingsProvider)
    {
        _databaseSettingsProvider = databaseSettingsProvider;
    }

    public Task InitializeAsync()
    {
        _container =_databaseSettingsProvider.GetContainer();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Update_Crew_Party_Members_Successfully()
    {
        // Arrange
        var sut = new CrewPartyCommands(_container!);
        _crewParty = CreateCrewParty();
        await sut.CreateCrewParty(_crewParty);
        var crewPartyMembers = new List<Player> {Player.Create("25", "John")};

        // Act
        await sut.UpdateMembers(_crewParty.Id, crewPartyMembers);

        // Assert
        var crewPartyResponse =
            await _container!.ReadItemAsync<CrewPartyDocument>(_crewParty.Id, new PartitionKey(_crewParty.Id));
        crewPartyResponse.Resource.Members.Should().BeEquivalentTo(crewPartyMembers);
    }

    private static CrewParty CreateCrewParty()
    {
        return new CrewParty(
            Player.Create("24", "Rowan"),
            new CrewName("Rowan"),
            Location.DefaultLocation(),
            LanguageCollections.Default(),
            new CrewCapacity(10, 10),
            Activity.Default());
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return CleanUpCrewParty();
    }

    private async Task CleanUpCrewParty()
    {
        if (_crewParty is null)
            return;

        await _container!.DeleteItemAsync<CrewParty>(_crewParty!.Id, new PartitionKey(_crewParty.Id));
    }
}