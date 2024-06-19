using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using BKA.Tools.CrewFinding.Crews.Ports;
using BKA.Tools.CrewFinding.Discord.Exceptions;
using BKA.Tools.CrewFinding.Discord.Models;

namespace BKA.Tools.CrewFinding.Discord;

public class VoiceChannelCommandRepository : IVoiceChannelCommandRepository
{ 
    private static readonly JsonSerializerOptions JsonOptions = new() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

    private readonly HttpClient _httpClient;
    private readonly long _guildId;
    private readonly long _parentId;

    public VoiceChannelCommandRepository(DiscordSettings discordSettings)
    {
        _httpClient = new HttpClient {BaseAddress = new Uri(DiscordSettings.GetBaseUrl())};
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", discordSettings.Token);
        _guildId = discordSettings.GuildId;
        _parentId = discordSettings.CrewParentId;
    }

    public async Task<string> Create(string name)
    {
        var request = new ChannelCreatorRequest(name, ChannelTypes.GuildVoice, _parentId);
        return (await CreateChannel(request)).Id;
    }

    private async Task<ChannelCreatorResponse> CreateChannel(ChannelCreatorRequest request)
    {
        var url = $"guilds/{_guildId}/channels";
        var content = CreateHttpContent(request);

        using var response = await _httpClient.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
            throw new DiscordCreationException();

        return await DeserializeResponseAsync<ChannelCreatorResponse>(response) ?? throw new DiscordCreationException();
    }

    private static HttpContent CreateHttpContent<T>(T request)
    {
        var jsonContent = JsonSerializer.Serialize(request, JsonOptions);
        return new StringContent(jsonContent, Encoding.UTF8, "application/json");
    }

    private static async Task<T> DeserializeResponseAsync<T>(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseContent, JsonOptions)!;
    }
}