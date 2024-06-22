using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Commands.Disbands;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Tests.Commons.Mocks;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks.Crews;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks.VoicedCrews;

namespace BKA.Tools.CrewFinding.Tests.Crews.Commands.Disbands;

public class VoicedCrewDisbandmentTest
{
    [Fact]
    public async Task Attempt_To_Disband_An_Not_Existing_Crew_Should_Throw_Exception()
    {
        // Arrange
        var sut = SetUpTest(new CrewDisbandmentNotFoundMock(),
            new VoiceChannelHandlerMock());

        // Act
        var act = async () => await sut.Disband(new CrewDisbandmentResponseMock());

        // Assert
        await act.Should().ThrowAsync<CrewNotFoundException>();
    }

    [Fact]
    public async Task Attempt_To_Disband_An_Existing_Crew_But_Without_Voice_Channel_Should_do_Nothing()
    {
        // Arrange
        const string crewId = "123412";
        var crewDisbandmentResponseMock = new CrewDisbandmentResponseMock();
        var voiceChannelCommandRepositoryMock = new VoiceChannelCommandRepositoryMock();

        var sut = SetUpTest(new CrewDisbandmentMock(crewId), new VoiceChannelHandlerMock());

        // Act
        await sut.Disband(crewDisbandmentResponseMock);

        // Assert
        crewDisbandmentResponseMock.CrewId.Should().NotBeNullOrEmpty();
        voiceChannelCommandRepositoryMock.RemovedCrewId.Should().BeEmpty();
    }

    [Fact]
    public async Task
        Attempt_To_Disband_An_Existing_Crew_With_Voice_Channel_But_VoiceChannel_delete_Fails_Should_Throw_Exception_Should_Store_A_Log()
    {
        // Arrange
        const string crewId = "123412";
        var crewDisbandmentResponseMock = new CrewDisbandmentResponseMock();
        var voiceChannelRepositoryMock = new VoiceChannelHandlerExceptionMock<Exception>();
        var crewDisbandmentMock = new CrewDisbandmentMock(crewId);
        var domainLoggerMock = new DomainLoggerMock(); 
        var voicedCrewQueryRepositoryMock = new VoiceChannelQueryRepositoryMock("312341");

        var sut = SetUpTest(crewDisbandmentMock, voiceChannelRepositoryMock, domainLoggerMock, voicedCrewQueryRepositoryMock);

        // Act
        await sut.Disband(crewDisbandmentResponseMock);

        // Assert
        voiceChannelRepositoryMock.DeletedChannelCallCount.Should().Be(1);
        domainLoggerMock.Message.Should().Be($"Voice channel of crew {crewId} could not be deleted.");
    }

    [Fact]
    public async Task VoicedCrew_Disbandment_Should_Disband_Crew_And_Delete_Voice_Channel_Successfully()
    {
        // Arrange
        const string crewId = "123412";
        const string voiceChannelId = "34423424";
        var crewDisbandmentResponseMock = new CrewDisbandmentResponseMock();
        var voiceChannelRepositoryMock = new VoiceChannelHandlerMock();
        var crewDisbandmentMock = new CrewDisbandmentMock(crewId);
        var voicedCrewQueryRepositoryMock = new VoiceChannelQueryRepositoryMock(voiceChannelId);
        var voicedCrewCommandRepositoryMock = new VoiceChannelCommandRepositoryMock();

        var sut = SetUpTest(crewDisbandmentMock, voiceChannelRepositoryMock, null, voicedCrewQueryRepositoryMock,
            voicedCrewCommandRepositoryMock);
        
        // Act
        await sut.Disband(crewDisbandmentResponseMock);

        // Assert
        crewDisbandmentResponseMock.CrewId.Should().Be(crewId);
        voiceChannelRepositoryMock.DeletedVoicedCrewsId.Should().Contain(voiceChannelId);
        voicedCrewCommandRepositoryMock.RemovedCrewId.Should().Be(crewId);
    }

    private static VoicedCrewDisbandment SetUpTest(ICrewDisbandment crewDisbandmentMock,
        IVoiceChannelHandler voiceChannelRepositoryMock,
        DomainLoggerMock? domainLoggerMock = null,
        IVoiceChannelQueryRepository? voiceChannelQueryRepositoryMock = null,
        IVoiceChannelCommandRepository? voiceChannelCommandRepositoryMock = null)
    {
        voiceChannelQueryRepositoryMock ??= new VoiceChannelQueryRepositoryEmptyMock();
        voiceChannelCommandRepositoryMock ??= new VoiceChannelCommandRepositoryMock();
        domainLoggerMock ??= new DomainLoggerMock();

        return new VoicedCrewDisbandment(crewDisbandmentMock, voiceChannelRepositoryMock,
            voiceChannelQueryRepositoryMock, domainLoggerMock, voiceChannelCommandRepositoryMock);
    }
}