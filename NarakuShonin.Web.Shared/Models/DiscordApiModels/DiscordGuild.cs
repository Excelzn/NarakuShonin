using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NarakuShonin.Web.Shared.Models.DiscordApiModels;

public class DiscordGuildLite
{
  [JsonPropertyName("id")]
  public string Id { get; set; }

  [JsonPropertyName("name")]
  public string Name { get; set; }

  [JsonPropertyName("icon")]
  public string Icon { get; set; }

  [JsonPropertyName("banner")]
  public string Banner { get; set; }

  [JsonPropertyName("owner")]
  public bool Owner { get; set; }

  [JsonPropertyName("permissions")]
  public ulong Permissions { get; set; }

  [JsonPropertyName("features")]
  public List<string> Features { get; set; }

  [JsonPropertyName("approximate_member_count")]
  public int ApproximateMemberCount { get; set; }

  [JsonPropertyName("approximate_presence_count")]
  public int ApproximatePresenceCount { get; set; }
}