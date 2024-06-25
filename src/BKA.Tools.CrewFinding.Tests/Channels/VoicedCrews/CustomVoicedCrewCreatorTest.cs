using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Channels;
using BKA.Tools.CrewFinding.Channels.Exceptions;
using BKA.Tools.CrewFinding.Crews;

namespace BKA.Tools.CrewFinding.Tests.Channels.VoicedCrews;

public class CustomVoicedCrewCreatorTest
{
    private const string RegexPattern = @"^https:\/\/discord\.gg\/([a-zA-Z0-9]{6,9})$";

    [Fact]
    public async Task
        Attempt_To_Add_A_CustomVoiceChannel_But_ChannelLink_Is_Empty_Should_Create_A_Default_VoiceChannel()
    {
        // Arrange
        var crewCreatorRequest = new VoicedCrewCreatorRequest(4, Location.Default(), ["ES"], Activity.Default().Name);
        var voiceChannelHandlerMock = new VoiceChannelHandlerMock();
        var voiceChannelCommandRepositoryMock = new VoiceChannelCommandRepositoryMock();

        var sut = InitializeSut(voiceChannelHandlerMock, voiceChannelCommandRepositoryMock);

        // Act
        await sut.Create(
            crewCreatorRequest,
            new CrewCreatorResponseMock());

        // Assert
        voiceChannelCommandRepositoryMock.CrewId.Should().NotBeNullOrEmpty();
        voiceChannelCommandRepositoryMock.VoiceChannelId.Should().NotBeNullOrEmpty();
    }

    [Theory]
    [InlineData("https://discord.gg/12345678313123123")]
    [InlineData("https://discord/12345678313123123")]
    [InlineData("https://eqweqweq.gg/12345678313123123")]
    [InlineData("Invalid")]
    public async Task Attempt_To_Add_A_CustomVoiceChannel_But_ChannelLink_Is_Invalid_Should_Throw_An_Exception(string channelLink)
    {
        // Arrange
        var crewCreatorRequest = new VoicedCrewCreatorRequest(4, Location.Default(), ["ES"], Activity.Default().Name,
            CustomChannelLink: channelLink);
        var voiceChannelHandlerMock = new VoiceChannelHandlerMock();
        var voiceChannelCommandRepositoryMock = new VoiceChannelCommandRepositoryMock();

        var sut = InitializeSut(voiceChannelHandlerMock, voiceChannelCommandRepositoryMock);

        // Act
        var act = async () => await sut.Create(
            crewCreatorRequest,
            new CrewCreatorResponseMock());

        // Assert
        await act.Should().ThrowAsync<CustomChannelFormatException>()
            .WithMessage($"Invalid channel link: {channelLink}");
        voiceChannelCommandRepositoryMock.CrewId.Should().BeEmpty();
        voiceChannelCommandRepositoryMock.VoiceChannelId.Should().BeEmpty();
    }
    
    [Fact]
    public async Task Attempt_To_Add_A_CustomVoiceChannel_But_ChannelLink_Is_Valid_Should_Create_A_Custom_VoiceChannel()
    {
        // Arrange
        const string channelLink = "https://discord.gg/123456";
        var crewCreatorRequest = new VoicedCrewCreatorRequest(4, Location.Default(), ["ES"], Activity.Default().Name,
            CustomChannelLink: channelLink);
        var voiceChannelHandlerMock = new VoiceChannelHandlerMock();
        var voiceChannelCommandRepositoryMock = new VoiceChannelCommandRepositoryMock();

        var crewCreatorResponseMock = new CrewCreatorResponseMock();
        var sut = InitializeSut(voiceChannelHandlerMock, voiceChannelCommandRepositoryMock);

        // Act
        await sut.Create(crewCreatorRequest, crewCreatorResponseMock);

        // Assert
        voiceChannelCommandRepositoryMock.CrewId.Should().NotBeNullOrEmpty();
        voiceChannelCommandRepositoryMock.VoiceChannelId.Should().Be(channelLink);
    }

    private static VoicedCrewCreator InitializeSut(VoiceChannelHandlerMock voiceChannelHandlerMock,
        VoiceChannelCommandRepositoryMock voiceChannelCommandRepositoryMock)
    {
        return new VoicedCrewCreator(new CrewCreatorMock("Test Crew"), voiceChannelHandlerMock,
            voiceChannelCommandRepositoryMock,
            new DomainLoggerMock(),
            RegexPattern);
    }
}