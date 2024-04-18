using System;
using System.Linq;
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

public class CreateCrewPartyTests : IAsyncLifetime
{
    private readonly IDatabaseSettingsProvider<Container> _databaseSettingsProvider;
    private Container? _container;
    private CrewParty? _crewParty;

    public CreateCrewPartyTests(IDatabaseSettingsProvider<Container> databaseSettingsProvider)
    {
        _databaseSettingsProvider = databaseSettingsProvider;
    }

    public Task InitializeAsync()
    {
        _container = _databaseSettingsProvider.GetContainer();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Create_A_Crew_Party_Successfully()
    {
        // Arrange
        var sut = CreateCrewPartyCommand();
        _crewParty = CreateCrewParty();

        // Act
        await sut.CreateCrewParty(_crewParty);

        // Assert
        await AssertCrewPartyWasCreatedSuccessfully(_crewParty);
    }

    private CrewPartyCommands CreateCrewPartyCommand()
    {
        return new CrewPartyCommands(_container!);
    }

    private static CrewParty CreateCrewParty()
    {
        var captain = Player.Create("24", "Rowan");
        
        return new CrewParty(
            captain,
            new CrewName("Rowan"),
            Location.DefaultLocation(),
            LanguageCollections.Default(),
            new CrewCapacity(10, 10),
            Activity.Default());
    }

    private async Task AssertCrewPartyWasCreatedSuccessfully(CrewParty crewParty)
    {
        var container = _databaseSettingsProvider.GetContainer();
        var crewPartyResponse =
            await container.ReadItemAsync<CrewPartyDocument>(crewParty.Id, new PartitionKey(crewParty.Id));

        crewPartyResponse.Should().NotBeNull();
        ValidateCrewPartyData(crewParty, crewPartyResponse);
    }

    private static void ValidateCrewPartyData(CrewParty crewParty, Response<CrewPartyDocument> crewPartyResponse)
    {
        crewPartyResponse.Resource.Id.Should().Be(crewParty.Id);
        crewPartyResponse.Resource.CrewName.Should().Be(crewParty.Name.Value);
        crewPartyResponse.Resource.CaptainId.Should().Be(crewParty.Captain.Id);
        
        ValidateLocation(crewParty, crewPartyResponse);
        ValidateLanguage(crewParty, crewPartyResponse);
        ValidateActivity(crewParty, crewPartyResponse);
        ValidateCrewCapacity(crewParty, crewPartyResponse);
    }

    private static void ValidateLocation(CrewParty crewParty, Response<CrewPartyDocument> crewPartyResponse)
    {
        crewPartyResponse.Resource.Place.Should().BeEquivalentTo(crewParty.ReunionPoint.Place);
        crewPartyResponse.Resource.System.Should().BeEquivalentTo(crewParty.ReunionPoint.System);
        crewPartyResponse.Resource.PlanetarySystem.Should().BeEquivalentTo(crewParty.ReunionPoint.PlanetarySystem);
        crewPartyResponse.Resource.PlanetMoon.Should().BeEquivalentTo(crewParty.ReunionPoint.PlanetMoon);
    }

    private static void ValidateLanguage(CrewParty crewParty, Response<CrewPartyDocument> crewPartyResponse)
    {
        crewPartyResponse.Resource.Language.Should()
            .BeEquivalentTo(crewParty.Languages.Select(l => l.LanguageCode).ToArray());
    }

    private static void ValidateActivity(CrewParty crewParty, Response<CrewPartyDocument> crewPartyResponse)
    {
        crewPartyResponse.Resource.Description.Should().BeEquivalentTo(crewParty.Activity.Description);
        crewPartyResponse.Resource.Activity.Should().BeEquivalentTo(crewParty.Activity.Name);
        crewPartyResponse.Resource.CreationAt.Should().Be(crewParty.CreationAt);
    }

    private static void ValidateCrewCapacity(CrewParty crewParty, Response<CrewPartyDocument> crewPartyResponse)
    {
        crewPartyResponse.Resource.Current.Should().Be(crewParty.CrewCapacity.Current);
        crewPartyResponse.Resource.MaxAllowed.Should().Be(crewParty.CrewCapacity.MaxAllowed);
    }

    public Task DisposeAsync()
    {
        return CleanUpCrewParty();
    }

    private async Task CleanUpCrewParty()
    {
        if (_crewParty is null)
            return;
        
        var container = _databaseSettingsProvider.GetContainer();
        await container.DeleteItemAsync<CrewParty>(_crewParty.Id, new PartitionKey(_crewParty.Id));
    }
}