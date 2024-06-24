using System;
using System.Threading.Tasks;
using BKA.Tools.CrewFinding.Channels;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;
using BKA.Tools.CrewFinding.Crews.Exceptions;
using BKA.Tools.CrewFinding.Crews.Ports;

namespace BKA.Tools.CrewFinding.Tests.Channels.VoicedCrews;

public class VoicedCrewCreatorTest
{
    
    private CrewCreatorRequest _crewCreatorRequest = BuildCrewCreatorRequest();
    
    [Fact]
    public async Task Attempt_To_Create_A_VoicedCrew_But_It_Fail_Should_Throw_Exception()
    {
        // Arrange
        _crewCreatorRequest = new CrewCreatorRequest(4, Location.Default(), ["ES"], Activity.Default().Name);
        var crewCreatorExceptionMock = new CrewCreatorExceptionMock<CrewNotFoundException>();
        var voiceChannelCommandRepositoryExceptionMock = new VoiceChannelHandlerExceptionMock<Exception>();

        var sut = InitializeSut(crewCreatorExceptionMock, voiceChannelCommandRepositoryExceptionMock);

        // Act
        await Assert.ThrowsAsync<CrewNotFoundException>(() => sut.Create(
            _crewCreatorRequest,
            new CrewCreatorResponseMock()));
    }

    [Fact]
    public async Task Attempt_To_Create_A_VoicedCrew_But_VoiceChannel_Fails_Should_Record_A_Log()
    {
        // Arrange
        const string crewName = "Crew of Rowan";
        var crewCreatorResponseMock = new CrewCreatorResponseMock();
        var domainLoggerMock = new DomainLoggerMock();

        var sut = InitializeSut(new CrewCreatorMock(crewName),
            new VoiceChannelHandlerExceptionMock<Exception>(),
            domainLogger: domainLoggerMock);

        // Act
        await sut.Create(
            _crewCreatorRequest,
            crewCreatorResponseMock);

        // Assert
        crewCreatorResponseMock.Id.Should().NotBeNullOrEmpty();
        crewCreatorResponseMock.Name.Should().Be(crewName);
        domainLoggerMock.E.Should().NotBeNull();
        domainLoggerMock.E.Should().BeOfType<Exception>();
        domainLoggerMock.Message.Should().NotBeNullOrEmpty();
        domainLoggerMock.Message.Should()
            .Be($"Failed to create voice channel from crew with id: {crewCreatorResponseMock.Id}");
    }

    [Fact]
    public async Task Create_A_VoicedCrew_Should_Add_A_VoiceChannelId_To_The_Crew_Successfully()
    {
        // Arrange
        const string crewName = "Crew of Rowan";
        var crewCreatorResponseMock = new CrewCreatorResponseMock();
        var domainLoggerMock = new DomainLoggerMock();
        var voicedCrewCommandRepositoryMock = new VoiceChannelCommandRepositoryMock();

        var sut = InitializeSut(new CrewCreatorMock(crewName), new VoiceChannelHandlerMock(),
            voicedCrewCommandRepositoryMock, domainLoggerMock);

        // Act
        await sut.Create(
            _crewCreatorRequest,
            crewCreatorResponseMock);

        // Assert
        voicedCrewCommandRepositoryMock.CrewId.Should().Be(crewCreatorResponseMock.Id);
        voicedCrewCommandRepositoryMock.VoiceChannelId.Should().NotBeNullOrEmpty();
        domainLoggerMock.E.Should().BeNull();
        domainLoggerMock.Message.Should().BeNullOrEmpty();
    }

    private static CrewCreatorRequest BuildCrewCreatorRequest()
    {
        var crewCreatorRequest = new CrewCreatorRequest(4, Location.Default(), ["ES"], Activity.Default().Name);
        return crewCreatorRequest;
    }

    private static VoicedCrewCreator InitializeSut(
        ICrewCreator? crewCreator = null,
        IVoiceChannelHandler? voiceChannelHandler = null,
        VoiceChannelCommandRepositoryMock? voiceChannelCommandRepositoryMock = null,
        IDomainLogger? domainLogger = null)
    {
        crewCreator = crewCreator ?? new CrewCreatorMock("Crew of Rowan");
        voiceChannelHandler = voiceChannelHandler ?? new VoiceChannelHandlerMock();
        voiceChannelCommandRepositoryMock = voiceChannelCommandRepositoryMock ?? new VoiceChannelCommandRepositoryMock();
        domainLogger = domainLogger ?? new DomainLoggerMock();

        var sut = new VoicedCrewCreator(
            crewCreator,
            voiceChannelHandler,
            voiceChannelCommandRepositoryMock,
            domainLogger);
        return sut;
    }
}