using System;
using System.Linq;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.CrewParties;
using BKA.Tools.CrewFinding.Azure.DataBase.Models;
using BKA.Tools.CrewFinding.CrewParties;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;
using BKA.Tools.CrewFinding.Values;
using FluentAssertions;
using Microsoft.Azure.Cosmos;
using Xunit;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests;

public class CrewPartyCommandTests
{
    private readonly IDatabaseSettingsProvider<Container> _databaseSettingsProvider;

    public CrewPartyCommandTests(IDatabaseSettingsProvider<Container> databaseSettingsProvider)
    {
        _databaseSettingsProvider = databaseSettingsProvider;
    }
    
    [Fact]
    public async Task Create_A_Crew_Party_Successfully()
    {
        // Arrange
        var container = _databaseSettingsProvider.GetContainer();
        var sut = new CrewPartyCommand(container);
        
        var crewParty = new CrewParty(new Player("24", "Rowan"), new CrewName("Rowan"), Location.DefaultLocation(), LanguageCollections.Default(), new CrewCapacity(10, 10), Activity.Default(), DateTime.UtcNow);

        // Act
        await sut.CreateCrewParty(crewParty);

        // Assert
        var crewPartyResponse = await container.ReadItemAsync<CrewPartyDocument>(crewParty.Id, new PartitionKey(crewParty.Id));
        crewPartyResponse.Should().NotBeNull();
        crewPartyResponse.Resource.Id.Should().Be(crewParty.Id);
        crewPartyResponse.Resource.CrewName.Should().Be(crewParty.Name.Value);
        crewPartyResponse.Resource.CaptainId.Should().Be(crewParty.Captain.Id);
        crewPartyResponse.Resource.Place.Should().BeEquivalentTo(crewParty.ReunionPoint.Place);
        crewPartyResponse.Resource.System.Should().BeEquivalentTo(crewParty.ReunionPoint.System);
        crewPartyResponse.Resource.PlanetarySystem.Should().BeEquivalentTo(crewParty.ReunionPoint.PlanetarySystem);
        crewPartyResponse.Resource.PlanetMoon.Should().BeEquivalentTo(crewParty.ReunionPoint.PlanetMoon);
        crewPartyResponse.Resource.Language.Should().BeEquivalentTo(crewParty.Languages.Select(l => l.LanguageCode).ToArray());
        crewPartyResponse.Resource.Description.Should().BeEquivalentTo(crewParty.Activity.Description);
        crewPartyResponse.Resource.Activity.Should().BeEquivalentTo(crewParty.Activity.Name);
        crewPartyResponse.Resource.CreationAt.Should().Be(crewParty.CreationAt);
        crewPartyResponse.Resource.Current.Should().Be(crewParty.CrewCapacity.Current);
        crewPartyResponse.Resource.MaxAllowed.Should().Be(crewParty.CrewCapacity.MaxAllowed);
        
        // Clean up
        await container.DeleteItemAsync<CrewParty>(crewParty.Id, new PartitionKey(crewParty.Id));
    }
}