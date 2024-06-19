using System.Text.Json.Serialization;

namespace BKA.Tools.CrewFinding.Discord.Models;

public record ChannelCreatorRequest(
    string Name,
    ChannelTypes Type,
    [property: JsonPropertyName("parent_id")]
    long ParentId);