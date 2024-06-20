using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Discord.Tests;

public class ChannelCreatorTest(IVoiceChannelCommandRepository voiceChannelCommandRepository) : IAsyncLifetime
{
    private const string ChannelName = "Test Crew Channel";
    private string? _channelId;

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Create_A_VoiceChannel_Successfully()
    {
        // Act
        _channelId = await voiceChannelCommandRepository.Create(ChannelName);

        // Assert
        _channelId.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Delete_A_VoiceChannel_Successfully()
    {
        // Arrange
        _channelId = await voiceChannelCommandRepository.Create(ChannelName);

        // Act
        await voiceChannelCommandRepository.Delete(_channelId);

        // Assert
        ((VoiceChannelCommandRepository) voiceChannelCommandRepository).ChannelExists(_channelId).Should().BeFalse();

        // Teardown
        _channelId = null;
    }

    public Task DisposeAsync()
    {
        return _channelId == null ? Task.CompletedTask : voiceChannelCommandRepository.Delete(_channelId!);
    }
}