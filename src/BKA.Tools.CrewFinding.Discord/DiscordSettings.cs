namespace BKA.Tools.CrewFinding.Discord;

public record DiscordSettings(string Token, long GuildId, long CrewParentId)
{
    public static string GetBaseUrl() => "https://discord.com/api/v10/";
}