using System.Net;
using BKA.Tools.CrewFinding.Crews;
using BKA.Tools.CrewFinding.Crews.Commands.Creators;
using BKA.Tools.CrewFinding.Discord.Exceptions;
using BKA.Tools.CrewFinding.Discord.Models;
using BKA.Tools.CrewFinding.Discord.Tests.Mocks;

namespace BKA.Tools.CrewFinding.Discord.Tests;

public class ChannelCreatorTest(IDiscordHttpClient discordHttpClient, DiscordSettings discordSettings)
{
    [Fact]
    public async Task Attempt_To_Create_A_VoiceChannel_But_Crew_Creation_Fails()
    {
        // Arrange
        var crewCreatorExceptionMock = new CrewCreatorExceptionMock();
        var sut = InitializeSut(crewCreatorExceptionMock, discordHttpClient);

        // Act & Assert
        await Assert.ThrowsAsync<NotImplementedException>(() =>
            sut.Create(new CrewCreatorRequest(4, Location.Default(), ["ES"], "Piracy"), new CrewCreatorResponseMock()));
    }

    [Fact]
    public async Task Attempt_To_Create_A_VoiceChannel_But_VoiceChannel_Creation_Fails()
    {
        // Arrange
        const string captainName = "Rowan";
        var crewCreatorMock = new CrewCreatorMock(captainName);
        var sut = InitializeSut(crewCreatorMock,
            new DiscordHttpClient(new DiscordSettings("token", "guildId", "crewParentId")));

        // Act & Assert
        await Assert.ThrowsAsync<DiscordCreationException>(() =>
            sut.Create(new CrewCreatorRequest(4, Location.Default(), ["ES"], "Piracy"), new CrewCreatorResponseMock()));
    }

    [Fact]
    public async Task Create_A_VoiceChannel_Successfully()
    {
        // Arrange
        const string captainName = "Rowan";
        var crewCreatorMock = new CrewCreatorMock(captainName);
        var crewCreatorResponseMock = new CrewCreatorResponseMock();
        var sut = InitializeSut(crewCreatorMock, discordHttpClient);

        // Act
        await sut.Create(new CrewCreatorRequest(4, Location.Default(), ["ES"], "Piracy"), crewCreatorResponseMock);

        // Assert
        sut.DiscordChannelId.Should().NotBeEmpty();
        crewCreatorResponseMock.GetName().Should().Contain(captainName);
    }

    private DiscordCrewCreator InitializeSut(ICrewCreator crewCreator, IDiscordHttpClient httpClient)
    {
        return new DiscordCrewCreator(crewCreator, httpClient, discordSettings);
    }
}

public class DiscordCrewCreator(
    ICrewCreator crewCreator,
    IDiscordHttpClient discordHttpClient,
    DiscordSettings discordSettings) : ICrewCreator
{
    public string DiscordChannelId { get; private set; } = string.Empty;

    public async Task Create(CrewCreatorRequest request, ICrewCreatorResponse output)
    {
        await crewCreator.Create(request, output);
        DiscordChannelId = await CreateVoiceChannel(output.GetName());
    }

    private async Task<string> CreateVoiceChannel(string name)
    {
        var channelResponse =
            await discordHttpClient.PostCreateChannelAsync(new ChannelCreatorRequest(name, ChannelTypes.GuildVoice,
                discordSettings.CrewParentId));

        return channelResponse.Id;
    }
}