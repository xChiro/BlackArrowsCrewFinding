using System.Linq;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties.Documents;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Cultures;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Values;
using FluentAssertions;
using Microsoft.Azure.Cosmos;
using Xunit;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.CrewParties;

public class CreateCrewPartyTests : IAsyncLifetime
{
    private readonly IDatabaseSettingsProvider<Container> _databaseSettingsProvider;
    private Container? _container;
    private string _crewPartyId = string.Empty;

    public CreateCrewPartyTests(IDatabaseSettingsProvider<Container> databaseSettingsProvider)
    {
        _databaseSettingsProvider = databaseSettingsProvider;
    }

    public Task InitializeAsync()
    {
        _container = _databaseSettingsProvider.GetCrewPartyContainer();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Create_A_Crew_Party_Successfully()
    {
        // Arrange
        var sut = CreateCrewPartyCommand();
        var crewParty = CreateCrewParty();
        _crewPartyId = crewParty.Id;

        // Act
        await sut.CreateCrew(crewParty);

        // Assert
        await AssertCrewPartyWasCreatedSuccessfully(crewParty);
    }

    private CrewCommands CreateCrewPartyCommand()
    {
        return new CrewCommands(_container!);
    }

    private static Crew CreateCrewParty()
    {
        var captain = Player.Create("24", "Rowan");
        
        return new Crew(
            captain,
            new CrewName("Rowan"),
            Location.DefaultLocation(),
            LanguageCollections.Default(),
            Members.CreateSingle(Player.Create("1", "wrerwerwe"), 1),
            Activity.Default());
    }

    private async Task AssertCrewPartyWasCreatedSuccessfully(Crew crew)
    {
        var container = _databaseSettingsProvider.GetCrewPartyContainer();
        var crewPartyResponse =
            await container.ReadItemAsync<CrewDocument>(crew.Id, new PartitionKey(crew.Id));

        crewPartyResponse.Should().NotBeNull();
        ValidateCrewPartyData(crew, crewPartyResponse);
    }

    private static void ValidateCrewPartyData(Crew crew, Response<CrewDocument> crewPartyResponse)
    {
        crewPartyResponse.Resource.Id.Should().Be(crew.Id);
        crewPartyResponse.Resource.CrewName.Should().Be(crew.Name.Value);
        crewPartyResponse.Resource.CaptainId.Should().Be(crew.Captain.Id);
        
        ValidateLocation(crew, crewPartyResponse);
        ValidateLanguage(crew, crewPartyResponse);
        ValidateActivity(crew, crewPartyResponse);
        ValidateCrewCapacity(crew, crewPartyResponse);
    }

    private static void ValidateLocation(Crew crew, Response<CrewDocument> crewPartyResponse)
    {
        crewPartyResponse.Resource.Place.Should().BeEquivalentTo(crew.ReunionPoint.Place);
        crewPartyResponse.Resource.System.Should().BeEquivalentTo(crew.ReunionPoint.System);
        crewPartyResponse.Resource.PlanetarySystem.Should().BeEquivalentTo(crew.ReunionPoint.PlanetarySystem);
        crewPartyResponse.Resource.PlanetMoon.Should().BeEquivalentTo(crew.ReunionPoint.PlanetMoon);
    }

    private static void ValidateLanguage(Crew crew, Response<CrewDocument> crewPartyResponse)
    {
        crewPartyResponse.Resource.Language.Should()
            .BeEquivalentTo(crew.Languages.Select(l => l.LanguageCode).ToArray());
    }

    private static void ValidateActivity(Crew crew, Response<CrewDocument> crewPartyResponse)
    {
        crewPartyResponse.Resource.Description.Should().BeEquivalentTo(crew.Activity.Description);
        crewPartyResponse.Resource.Activity.Should().BeEquivalentTo(crew.Activity.Name);
        crewPartyResponse.Resource.CreationAt.Should().Be(crew.CreationAt);
    }

    private static void ValidateCrewCapacity(Crew crew, Response<CrewDocument> crewPartyResponse)
    {
        crewPartyResponse.Resource.Members.Count.Should().Be(crew.Members.Count());
        crewPartyResponse.Resource.MaxAllowed.Should().Be(crew.Members.MaxAllowed);
    }

    public Task DisposeAsync()
    {
        return CleanUpCrewParty();
    }

    private async Task CleanUpCrewParty()
    {
        if (_crewPartyId == string.Empty)
            return;
        
        var container = _databaseSettingsProvider.GetCrewPartyContainer();
        await container.DeleteItemAsync<Crew>(_crewPartyId, new PartitionKey(_crewPartyId));
    }
}