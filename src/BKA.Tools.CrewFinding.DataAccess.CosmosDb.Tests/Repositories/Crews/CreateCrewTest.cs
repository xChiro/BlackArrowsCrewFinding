using System.Linq;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties.Documents;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.Crews;

public class CreateCrewTest : IAsyncLifetime
{
    private readonly ICrewCommandRepository _sut;
    private readonly IDatabaseSettingsProvider<Container> _databaseSettingsProvider;
    private Container? _crewContainer;
    private string _crewId = string.Empty;

    public CreateCrewTest(ICrewCommandRepository sut, IDatabaseSettingsProvider<Container>  databaseSettingsProvider)
    {
        _sut = sut;
        _databaseSettingsProvider = databaseSettingsProvider;
    }

    public Task InitializeAsync()
    {
        _crewContainer = _databaseSettingsProvider.GetCrewContainer();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Create_A_Crew_Successfully()
    {
        // Arrange
        var crew = CrewBuilder.CreateDefaultCrew();
        _crewId = crew.Id;

        // Act
        await _sut.CreateCrew(crew);

        // Assert
        await AssertCrewWasCreatedSuccessfully(crew);
    }

    private async Task AssertCrewWasCreatedSuccessfully(Crew crew)
    {
        var crewPartyResponse =
            await _crewContainer!.ReadItemAsync<CrewDocument>(crew.Id, new PartitionKey(crew.Id));

        crewPartyResponse.Should().NotBeNull();
        ValidateCrewPartyData(crew, crewPartyResponse);
    }

    private static void ValidateCrewPartyData(Crew crew, Response<CrewDocument> crewPartyResponse)
    {
        crewPartyResponse.Resource.Id.Should().Be(crew.Id);
        crewPartyResponse.Resource.CrewName.Should().Be(crew.Name.Value);
        
        ValidateCaptain(crew, crewPartyResponse);
        ValidateLocation(crew, crewPartyResponse);
        ValidateLanguage(crew, crewPartyResponse);
        ValidateActivity(crew, crewPartyResponse);
        ValidateCrewCapacity(crew, crewPartyResponse);
    }

    private static void ValidateCaptain(Crew crew, Response<CrewDocument> crewPartyResponse)
    {
        crewPartyResponse.Resource.CaptainId.Should().Be(crew.Captain.Id);
        crewPartyResponse.Resource.CaptainName.Should().Be(crew.Captain.CitizenName);
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
        crewPartyResponse.Resource.ActivityDescription.Should().BeEquivalentTo(crew.Activity.Description);
        crewPartyResponse.Resource.ActivityName.Should().BeEquivalentTo(crew.Activity.Name);
        crewPartyResponse.Resource.CreationAt.Should().Be(crew.CreatedAt);
    }

    private static void ValidateCrewCapacity(Crew crew, Response<CrewDocument> crewPartyResponse)
    {
        crewPartyResponse.Resource.Crew.Count.Should().Be(crew.Players.Count());
        crewPartyResponse.Resource.MaxAllowed.Should().Be(crew.Players.MaxSize);
    }

    public Task DisposeAsync()
    {
        return CleanUpCrewParty();
    }

    private async Task CleanUpCrewParty()
    {
        if (_crewId == string.Empty)
            return;
        
        await _crewContainer!.DeleteItemAsync<Crew>(_crewId, new PartitionKey(_crewId));
    }
}