using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.VoiceChannels.Documents;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.VoiceChannels;

public class RemoveVoiceChannelTest(
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
        await databaseSettingsProvider.GetVoiceChannelContainer().CreateItemAsync(new VoiceChannelDocument
        {
            Id = _crewId,
            ChannelId = voiceId,
            CrewId = _crewId,
            CreateAt = DateTime.UtcNow
        }, new PartitionKey(_crewId));

        // Act
        await voiceChannelCommandRepository.RemoveChannel(_crewId);

        // Assert
        using var response = await _container!.ReadItemStreamAsync(_crewId, new PartitionKey(_crewId));
        response.IsSuccessStatusCode.Should().BeFalse();
        
        // Tear down
        _crewId = string.Empty;
    }

    public async Task DisposeAsync()
    {
        if (_crewId == string.Empty)
            return;

        await _container!.DeleteItemAsync<VoiceChannelDocument>(_crewId, new PartitionKey(_crewId));
    }
    
}