using Newtonsoft.Json;
using System.Collections.Generic;

namespace NarakuShonin.Web.Shared.Models.DiscordApiModels;

public class DiscordGuildLite
{
  [JsonProperty(PropertyName = "id")]
  public string Id { get; set; }

  [JsonProperty(PropertyName = "name")]
  public string Name { get; set; }

  [JsonProperty(PropertyName = "icon")]
  public string Icon { get; set; }

  [JsonProperty(PropertyName = "banner")]
  public string Banner { get; set; }

  [JsonProperty(PropertyName = "owner")]
  public bool Owner { get; set; }

  [JsonProperty(PropertyName = "permissions")]
  public ulong Permissions { get; set; }

  [JsonProperty(PropertyName = "features")]
  public List<string> Features { get; set; }

  [JsonProperty(PropertyName = "approximate_member_count")]
  public int ApproximateMemberCount { get; set; }

  [JsonProperty(PropertyName = "approximate_presence_count")]
  public int ApproximatePresenceCount { get; set; }
}