using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.VoiceChannels.Documents;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.VoiceChannels;

public class SetVoiceChannelToCrewTest(
    IVoiceChannelCommandRepository voiceChannelCommandRepository,
    IDatabaseSettingsProvider<Container> databaseSettingsProvider)
    : IAsyncLifetime
{
    private Container? _container;
    private string _crewId = string.Empty;

    public Task InitializeAsync()
    {
        _container = databaseSettingsProvider.GetVoiceChannelContainer();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Set_Voice_Channel_To_Crew_Successfully()
    {
        // Arrange
        _crewId = Guid.NewGuid().ToString();
        var voiceId = Guid.NewGuid().ToString();

        // Act
        await voiceChannelCommandRepository.AddVoiceChannel(_crewId, voiceId);

        // Assert
        var crewResponse = await _container!.ReadItemAsync<VoiceChannelDocument>(_crewId, new PartitionKey(_crewId));
        crewResponse?.Resource.Should().NotBeNull();
        crewResponse!.Resource.CrewId.Should().Be(_crewId);
        crewResponse.Resource.ChannelId.Should().Be(voiceId);
        crewResponse.Resource.CreateAt.Should().BeCloseTo(DateTime.UtcNow, new TimeSpan(0, 0, 5));
    }

    public async Task DisposeAsync()
    {
        if (_crewId == string.Empty)
            return;

        await _container!.DeleteItemAsync<VoiceChannelDocument>(_crewId, new PartitionKey(_crewId));
    }
}