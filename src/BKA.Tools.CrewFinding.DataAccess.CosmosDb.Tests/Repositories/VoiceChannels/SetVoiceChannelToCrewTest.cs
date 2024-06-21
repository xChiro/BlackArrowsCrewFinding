using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.VoiceChannels.Documents;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.VoiceChannels;

public class SetVoiceChannelToCrewTest(
    IVoicedCrewCommandRepository voicedCrewCommandRepository,
    IDatabaseSettingsProvider<Container> databaseSettingsProvider)
    : IAsyncLifetime
{
    private Container? _container;

    private string _voiceId = string.Empty;

    public Task InitializeAsync()
    {
        _container = databaseSettingsProvider.GetVoiceChannelContainer();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Set_Voice_Channel_To_Crew_Successfully()
    {
        // Arrange
        _voiceId = Guid.NewGuid().ToString();
        const string crewId = "123312";

        // Act
        await voicedCrewCommandRepository.AddVoiceChannel(crewId, _voiceId);

        // Assert
        var crewResponse = await _container!.ReadItemAsync<VoiceChannelDocument>(_voiceId, new PartitionKey(_voiceId));
        crewResponse?.Resource.Should().NotBeNull();
        crewResponse!.Resource.CrewId.Should().Be(crewId);
        crewResponse.Resource.Id.Should().Be(_voiceId);
    }

    public async Task DisposeAsync()
    {
        await CleanUpCrewParty();
    }

    private async Task CleanUpCrewParty()
    {
        if (_voiceId == string.Empty)
            return;

        await _container!.DeleteItemAsync<VoiceChannelDocument>(_voiceId, new PartitionKey(_voiceId));
    }
}