using System.Text.Json.Serialization;

namespace BKA.Tools.CrewFinding.Discord.Models;

public record ChannelCreatorRequest(string Name, ChannelTypes Type, string ParentId)
{
    [JsonPropertyName("parent_id")] public string ParentId { get; init; } = ParentId;

    public ChannelTypes Type { get; init; } = Type;

    public string Name { get; init; } = Name;
}