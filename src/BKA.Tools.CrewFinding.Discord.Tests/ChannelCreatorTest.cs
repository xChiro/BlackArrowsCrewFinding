using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Discord.Tests;

public class ChannelCreatorTest(IVoiceChannelCommandRepository voiceChannelCommandRepository)
{
    [Fact]
    public async Task Create_A_VoiceChannel_Successfully()
    {
        // Arrange
        const string channelName = "Test Crew Channel";

        // Act
        var crewCreatorResponseMock = await voiceChannelCommandRepository.Create(channelName);

        // Assert
        crewCreatorResponseMock.Should().NotBeNullOrEmpty();
    }
}