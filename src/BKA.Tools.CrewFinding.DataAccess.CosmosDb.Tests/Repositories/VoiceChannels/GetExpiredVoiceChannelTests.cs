using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Azure.DataBase.VoiceChannels.Documents;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Settings;

namespace BKA.Tools.CrewFinding.DataAccess.CosmosDb.Tests.Repositories.VoiceChannels;

public class ExpiredVoiceChannelTests(
    IVoiceChannelQueryRepository voiceChannelQueryRepository,
    IDatabaseSettingsProvider<Container> databaseSettingsProvider)
    : IAsyncLifetime
{
    private Container? _container;
    private readonly List<VoiceChannelDocument> _voiceChannels = [];

    public Task InitializeAsync()
    {
        _container = databaseSettingsProvider.GetVoiceChannelContainer();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Get_ExpiredVoiceChannels_Successfully()
    {
        // Arrange
        var expiredCrewId = Guid.NewGuid().ToString();
        var crewIdOnTime = Guid.NewGuid().ToString();
        _voiceChannels.AddRange([
            new VoiceChannelDocument
            {
                Id = expiredCrewId,
                ChannelId = Guid.NewGuid().ToString(),
                CrewId = expiredCrewId,
                CreateAt = DateTime.UtcNow.AddHours(-3)
            },
            new VoiceChannelDocument
            {
                Id = crewIdOnTime,
                ChannelId = Guid.NewGuid().ToString(),
                CrewId = crewIdOnTime,
                CreateAt = DateTime.UtcNow
            }
        ]);

        await _container!.CreateItemAsync(_voiceChannels[0], new PartitionKey(_voiceChannels[0].CrewId));
        await _container.CreateItemAsync(_voiceChannels[1], new PartitionKey(_voiceChannels[1].CrewId));

        // Act
        var expiredChannels = await voiceChannelQueryRepository.GetExpiredChannels(2);

        // Assert
        expiredChannels.Should().HaveCount(1);
        expiredChannels[0].CrewId.Should().Be(expiredCrewId);
        expiredChannels[0].VoiceChannelId.Should().Be(_voiceChannels[0].ChannelId);
        expiredChannels[0].CrewId.Should().Be(_voiceChannels[0].CrewId);
    }

    public async Task DisposeAsync()
    {
        foreach (var channel in _voiceChannels)
        {
            await _container!.DeleteItemAsync<VoiceChannelDocument>(channel.Id, new PartitionKey(channel.CrewId));
        }
    }
}