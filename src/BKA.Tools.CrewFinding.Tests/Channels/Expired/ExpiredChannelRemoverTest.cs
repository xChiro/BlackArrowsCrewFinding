using System;
using System.Linq;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Channels;
using BKA.Tools.CrewFinding.Channels.Expired;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Tests.Channels.Mocks;

namespace BKA.Tools.CrewFinding.Tests.Channels.Expired;

public class ExpiredChannelRemoverTest
{
    private const int HoursThreshold = 3;
    
    [Fact]
    public async Task Attempt_To_Remove_ExpiredChannels_But_VoiceChannel_Delete_Fails_Should_Throw_Exception()
    {
        // Arrange
        var channelQueryRepositoryMock = new VoiceChannelQueryRepositoryExceptionMock<Exception>();
        var sut = InitializeSut(channelQueryRepositoryMock);

        // Act
        var act = async () => await sut.Remove();

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task Attempt_To_Remove_ExpiredChannels_But_There_Are_No_Expired_Channels_Should_Do_Nothing()
    {
        // Arrange
        var channelQueryRepositoryMock = new VoiceChannelQueryRepositoryEmptyMock();
        var channelCommandRepositoryMock = new VoiceChannelCommandRepositoryMock();

        var sut = InitializeSut(channelQueryRepositoryMock, channelCommandRepositoryMock);

        // Act
        await sut.Remove();

        // Assert
        channelQueryRepositoryMock.GetExpiredChannelCount.Should().Be(1);
    }

    [Fact]
    public async Task Attempt_To_Remove_ExpiredChannels_But_ChannelHandler_Delete_Fails_Should_Throw_Exception()
    {
        // Arrange";
        var channelQueryRepositoryMock = CreateVoiceChannelQueryRepositoryMock(new VoiceChannel("123", "312312"));
        var channelHandlerExceptionMock = new VoiceChannelHandlerExceptionMock<Exception>();
        var sut = InitializeSut(channelQueryRepositoryMock, null, channelHandlerExceptionMock);


        // Act
        var act = async () => await sut.Remove();

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task Attempt_To_Remove_ExpiredChannels_But_Repository_Delete_Fails_Should_Throw_Exception()
    {
        // Arrange
        var voiceChannels = new VoiceChannel("123", "312312");
        var channelQueryRepositoryMock = CreateVoiceChannelQueryRepositoryMock(voiceChannels);
        var channelCommandRepositoryMock = new VoiceChannelCommandRepositoryExceptionMock<ArgumentException>();
        var channelHandlerMock = new VoiceChannelHandlerMock();
        var sut = InitializeSut(channelQueryRepositoryMock, channelCommandRepositoryMock, channelHandlerMock);

        // Act
        var act = async () => await sut.Remove();

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
        channelHandlerMock.DeletedVoicedCrewIds.Should().BeEquivalentTo(new[] {voiceChannels.VoiceChannelId});
    }

    [Fact]
    public async Task Remove_ExpiredChannels_Successfully()
    {
        // Arrange
        VoiceChannel[] voiceChannelId =
        [
            new VoiceChannel("123", "312312"),
            new VoiceChannel("456", "312312")
        ];

        var channelQueryRepositoryMock = new VoiceChannelQueryRepositoryMock(voiceChannelId);
        var channelCommandRepositoryMock = new VoiceChannelCommandRepositoryMock();
        var channelHandlerMock = new VoiceChannelHandlerMock();

        var sut = InitializeSut(channelQueryRepositoryMock, channelCommandRepositoryMock, channelHandlerMock);

        // Act
        await sut.Remove();

        // Assert
        channelCommandRepositoryMock.RemovedCrewId.Should().BeEquivalentTo(voiceChannelId.Select(x => x.CrewId));
    }

    [Fact]
    public async Task Remove_CustomExpired_Channels_Successfully()
    {
        // Arrange
        VoiceChannel[] voiceChannels =
        [
            new VoiceChannel("123", "https://discord.gg/zeZBpvYv"),
            new VoiceChannel("456", "123312")
        ];

        var channelQueryRepositoryMock = new VoiceChannelQueryRepositoryMock(voiceChannels);
        var channelCommandRepositoryMock = new VoiceChannelCommandRepositoryMock();
        var channelHandlerMock = new VoiceChannelHandlerMock();

        var sut = InitializeSut(channelQueryRepositoryMock, channelCommandRepositoryMock, channelHandlerMock);

        // Act
        await sut.Remove();

        // Assert
        channelCommandRepositoryMock.RemovedCrewId.Should().BeEquivalentTo(voiceChannels.Select(x => x.CrewId));
        channelHandlerMock.DeletedVoicedCrewIds.Should().BeEquivalentTo([voiceChannels[1].VoiceChannelId]);
    }

    private static ExpiredChannelRemover InitializeSut(
        IVoiceChannelQueryRepository channelQueryRepository,
        IVoiceChannelCommandRepository? channelCommandRepository = null,
        IVoiceChannelHandler? channelHandler = null
    )
    {
        var commandRepository = channelCommandRepository ?? new VoiceChannelCommandRepositoryMock();
        var handler = channelHandler ?? new VoiceChannelHandlerMock();
        return new ExpiredChannelRemover(HoursThreshold, channelQueryRepository, commandRepository, handler);
    }

    private static VoiceChannelQueryRepositoryMock CreateVoiceChannelQueryRepositoryMock(
        params VoiceChannel[] voiceChannels)
    {
        return new VoiceChannelQueryRepositoryMock(voiceChannels);
    }
}