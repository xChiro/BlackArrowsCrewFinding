using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Channels;
using BKA.Tools.CrewFinding.Channels.Exceptions;
using BKA.Tools.CrewFinding.Channels.invites;
using BKA.Tools.CrewFinding.Commons.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Players;
using BKA.Tools.CrewFinding.Tests.Channels.Mocks;
using BKA.Tools.CrewFinding.Tests.Commons;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks.Crews;

namespace BKA.Tools.CrewFinding.Tests.Channels.Invites;

public class ChannelInviteLinkCreatorTest
{
    private const string CrewId = "123";
    private readonly UserSessionMock _userSessionMock = new("some-user-id");
    private readonly CrewQueryRepositoryMock _crewQueryRepositoryMock;
    private readonly ChannelInviteLinkCreatorResponseMock _responseMock = new();

    public ChannelInviteLinkCreatorTest()
    {
        _crewQueryRepositoryMock = CreateCrewQueryRepositoryMock();
    }

    [Fact]
    public void Attempt_To_Create_ChannelInviteLink_When_Player_Does_Not_Have_An_Active_Crew_Should_ThrowException()
    {
        // Arrange
        var sut = InitializeSut(new VoiceChannelHandlerExceptionMock<PlayerNotInCrewException>());

        // Act
        var act = async () => await sut.Create(_responseMock);

        // Assert
        act.Should().ThrowAsync<PlayerNotInCrewException>();
    }

    [Fact]
    public void Attempt_To_Create_ChannelInviteLink_When_There_Is_Not_VoiceChannel_Should_Throw()
    {
        // Arrange
        var sut = InitializeSut(new VoiceChannelHandlerExceptionMock<NotVoiceChannelException>());

        // Act
        var act = async () => await sut.Create(_responseMock);

        // Assert
        act.Should().ThrowAsync<NotVoiceChannelException>();
    }

    [Fact]
    public void
        Attempt_To_Create_ChannelInviteLink_When_Player_Has_An_Active_Crew_But_LinkCreation_Fails_Should_ThrowException()
    {
        // Arrange
        var sut = InitializeSut(new VoiceChannelHandlerExceptionMock<Exception>());

        // Act
        var act = async () => await sut.Create(_responseMock);

        // Assert
        act.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task Create_ChannelInviteLink_When_Player_Has_An_Active_VoicedCrew_Should_Return_Link()
    {
        // Arrange
        var sut = InitializeSut(new VoiceChannelHandlerMock());

        // Act
        await sut.Create(_responseMock);

        // Assert
        _responseMock.Link.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Create_ChannelInviteLink_When_Crew_Has_A_CustomChannelLink_Should_Return_CustomLink()
    {
        // Arrange
        const string customLink = "https://discord.gg/zeZBpvYv";
        var voiceChannelQueryRepositoryMock = new VoiceChannelQueryRepositoryMock([
            new VoiceChannel(CrewId, customLink)
        ]);

        var voiceChannelHandlerMock = new VoiceChannelHandlerMock();
        var sut = new ChannelInviteLinkCreator(_userSessionMock,
            voiceChannelHandlerMock,
            voiceChannelQueryRepositoryMock,
            _crewQueryRepositoryMock);

        // Act
        await sut.Create(_responseMock);

        // Assert
        _responseMock.Link.Should().Be(customLink);
        voiceChannelHandlerMock.CreateCallCounts.Should().Be(0);
    }

    private CrewQueryRepositoryMock CreateCrewQueryRepositoryMock()
    {
        var crew = CrewBuilder.Build(CrewId, Player.Create(_userSessionMock.GetUserId(), "captain", 2, 16));
        return new CrewQueryRepositoryMock(new[] {crew});
    }

    private ChannelInviteLinkCreator InitializeSut(IVoiceChannelHandler voiceChannelHandler)
    {
        return new ChannelInviteLinkCreator(_userSessionMock,
            voiceChannelHandler,
            new VoiceChannelQueryRepositoryMock(new[] {new VoiceChannel(CrewId, "123")}),
            _crewQueryRepositoryMock);
    }
}