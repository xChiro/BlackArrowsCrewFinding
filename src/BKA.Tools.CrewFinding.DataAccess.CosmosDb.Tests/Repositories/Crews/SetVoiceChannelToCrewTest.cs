using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.Repositories.CrewParties.Documents;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.Crews;

public class SetVoiceChannelToCrewTest(
    IVoicedCrewCommandRepository voicedCrewCommandRepository,
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
    public async Task Set_Voice_Channel_To_Crew_Successfully()
    {
        // Arrange
        const string voiceChannelId = "123";
        var crew = CrewBuilder.CreateDefaultCrew(4);
        _crewId = crew.Id;
        await _container!.CreateItemAsync(CrewDocument.CreateFromCrew(crew));

        // Act
        await voicedCrewCommandRepository.AddVoiceChannel(crew.Id, voiceChannelId);

        // Assert
        var crewResponse = await _container!.ReadItemAsync<CrewDocument>(crew.Id, new PartitionKey(crew.Id));
        crewResponse.Resource.VoiceChannelId.Should().Be(voiceChannelId);
    }

    public async Task DisposeAsync()
    {
        await CleanUpCrewParty();
    }

    private async Task CleanUpCrewParty()
    {
        if (_crewId == string.Empty)
            return;

        await _container!.DeleteItemAsync<Crew>(_crewId, new PartitionKey(_crewId));
    }
}