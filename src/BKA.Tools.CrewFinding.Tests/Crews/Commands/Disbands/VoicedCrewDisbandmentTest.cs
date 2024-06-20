using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Crews.Commands.Disbands;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Tests.Commons.Mocks;
using BKA.Tools.CrewFinding.Tests.Crews.Mocks.VoicedCrews;

namespace BKA.Tools.CrewFinding.Tests.Crews.Commands.Disbands;

public class VoicedCrewDisbandmentTest
{
    [Fact]
    public async Task Attempt_To_Disband_An_Not_Existing_Crew_Should_Throw_Exception()
    {
        // Arrange
        var sut = SutVoicedCrewDisbandment(new CrewDisbandmentNotFoundMock(),
            new VoiceChannelCommandRepositoryMock());

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
        var sut = SutVoicedCrewDisbandment(new CrewDisbandmentMock(crewId, null),
            new VoiceChannelCommandRepositoryMock());

        // Act
        await sut.Disband(crewDisbandmentResponseMock);

        // Assert
        crewDisbandmentResponseMock.CrewId.Should().NotBeNullOrEmpty();
        crewDisbandmentResponseMock.VoiceChannelId.Should().BeNull();
    }

    [Fact]
    public async Task
        Attempt_To_Disband_An_Existing_Crew_With_Voice_Channel_But_VoiceChannel_delete_Fails_Should_Throw_Exception_Should_Store_A_Log()
    {
        // Arrange
        const string crewId = "123412";
        const string? voiceChannelId = "1333";
        var crewDisbandmentResponseMock = new CrewDisbandmentResponseMock();
        var voiceChannelRepositoryMock = new VoiceChannelCommandRepositoryExceptionMock<Exception>();
        var crewDisbandmentMock = new CrewDisbandmentMock(crewId, voiceChannelId);
        var domainLoggerMock = new DomainLoggerMock();

        var sut = SutVoicedCrewDisbandment(crewDisbandmentMock, voiceChannelRepositoryMock, domainLoggerMock);

        // Act
        await sut.Disband(crewDisbandmentResponseMock);

        // Assert
        voiceChannelRepositoryMock.DeletedChannelCallCount.Should().Be(1);
        domainLoggerMock.Message.Should().Be($"Voice channel with id {voiceChannelId} could not be deleted.");
    }
    
    [Fact]
    public async Task VoicedCrew_Disbandment_Should_Disband_Crew_And_Delete_Voice_Channel_Successfully()
    {
        // Arrange
        const string crewId = "123412";
        const string voiceChannelId = "1333";
        var crewDisbandmentResponseMock = new CrewDisbandmentResponseMock();
        var voiceChannelRepositoryMock = new VoiceChannelCommandRepositoryMock();
        var crewDisbandmentMock = new CrewDisbandmentMock(crewId, voiceChannelId);

        var sut = SutVoicedCrewDisbandment(crewDisbandmentMock, voiceChannelRepositoryMock);

        // Act
        await sut.Disband(crewDisbandmentResponseMock);

        // Assert
        crewDisbandmentResponseMock.CrewId.Should().Be(crewId);
        crewDisbandmentResponseMock.VoiceChannelId.Should().Be(voiceChannelId);
        voiceChannelRepositoryMock.DeletedChannelIds.Should().Contain(voiceChannelId);
    }

    private static VoicedCrewDisbandment SutVoicedCrewDisbandment(ICrewDisbandment crewDisbandmentMock,
        IVoiceChannelCommandRepository voiceChannelRepositoryMock)
    {
        return SutVoicedCrewDisbandment(crewDisbandmentMock, voiceChannelRepositoryMock, new DomainLoggerMock());
    }

    private static VoicedCrewDisbandment SutVoicedCrewDisbandment(ICrewDisbandment crewDisbandmentMock,
        IVoiceChannelCommandRepository voiceChannelRepositoryMock, IDomainLogger domainLoggerMock)
    {
        var voicedCrewDisbandment =
            new VoicedCrewDisbandment(crewDisbandmentMock, voiceChannelRepositoryMock, domainLoggerMock);

        return voicedCrewDisbandment;
    }
}

public class VoicedCrewDisbandment(
    ICrewDisbandment crewDisbandment,
    IVoiceChannelCommandRepository voiceChannelRepository,
    IDomainLogger domainLoggerMock)
    : ICrewDisbandment, ICrewDisbandmentResponse
{
    private string _crewId = string.Empty;
    private string? _voiceChannelId;

    public async Task Disband(ICrewDisbandmentResponse? output = null)
    {
        await crewDisbandment.Disband(this);

        try
        {
            if (_voiceChannelId is not null)
                await voiceChannelRepository.Delete(_voiceChannelId);
        }
        catch (Exception e)
        {
            domainLoggerMock.Log(e, $"Voice channel with id {_voiceChannelId} could not be deleted.");
        }
        finally
        {
            output?.SetResult(_crewId, _voiceChannelId);
        }
    }

    public void SetResult(string crewId, string? voiceChannelId)
    {
        _crewId = crewId;
        _voiceChannelId = voiceChannelId;
    }
}

public class CrewDisbandmentMock(string crewId, string? voiceChannelId) : ICrewDisbandment
{
    public Task Disband(ICrewDisbandmentResponse? output = null)
    {
        output?.SetResult(crewId, voiceChannelId);
        return Task.CompletedTask;
    }
}

public class CrewDisbandmentNotFoundMock : ICrewDisbandment
{
    public Task Disband(ICrewDisbandmentResponse? output = null)
    {
        throw new CrewNotFoundException();
    }
}