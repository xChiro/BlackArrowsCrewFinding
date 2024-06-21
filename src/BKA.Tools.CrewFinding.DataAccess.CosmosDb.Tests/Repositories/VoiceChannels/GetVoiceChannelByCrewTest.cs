using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.VoiceChannels.Documents;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.VoiceChannels;

public class GetVoiceChannelByCrewTest(
    IVoiceChannelQueryRepository voiceChannelQueryRepository,
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
    public async Task Get_VoiceChannel_By_CrewId_Successfully()
    {
        // Arrange
        _crewId = Guid.NewGuid().ToString();
        var voiceChannelId = Guid.NewGuid().ToString();
        
        await _container!.CreateItemAsync(new VoiceChannelDocument
        {
            Id = _crewId,
            ChannelId = voiceChannelId,
            CrewId = _crewId,
            CreateAt = DateTime.UtcNow
        }, new PartitionKey(_crewId));
    
        // Act
        var voiceChannel = await voiceChannelQueryRepository.GetVoiceChannelIdByCrewId(_crewId);

        // Assert
        voiceChannel.Should().NotBeNull();
    }

    public async Task DisposeAsync()
    {
        if (_crewId == string.Empty)
            return;

        await _container!.DeleteItemAsync<VoiceChannelDocument>(_crewId, new PartitionKey(_crewId));
    }
}