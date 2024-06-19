using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using BKA.Tools.CrewFinding.Discord.Exceptions;
using BKA.Tools.CrewFinding.Discord.Models;

namespace BKA.Tools.CrewFinding.Discord;

public class DiscordHttpClient : IDiscordHttpClient
{
    private static readonly JsonSerializerOptions JsonOptions = new() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

    private readonly HttpClient _httpClient;
    private readonly string _guildId;

    public DiscordHttpClient(DiscordSettings discordSettings)
    {
        _httpClient = new HttpClient {BaseAddress = new Uri(DiscordSettings.GetBaseUrl())};
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bot", discordSettings.Token);
        _guildId = discordSettings.GuildId;
    }

    public async Task<ChannelCreatorResponse> PostCreateChannelAsync(ChannelCreatorRequest request)
    {
        var url = $"guilds/{_guildId}/channels";
        var content = CreateHttpContent(request);

        using var response = await _httpClient.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
            throw new DiscordCreationException();

        return await DeserializeResponseAsync<ChannelCreatorResponse>(response) ?? throw new DiscordCreationException();
    }

    private static HttpContent CreateHttpContent(ChannelCreatorRequest request)
    {
        var jsonContent = JsonSerializer.Serialize(request, JsonOptions);
        return new StringContent(jsonContent, Encoding.UTF8, "application/json");
    }

    private static async Task<T> DeserializeResponseAsync<T>(HttpResponseMessage response)
    {
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseContent)!;
    }
}