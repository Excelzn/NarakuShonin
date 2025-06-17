using System.Text.Json.Serialization;
using AutoMapper;
using NarakuShonin.Web.Shared.Models.DiscordApiModels;

namespace NarakuShonin.Api.Models.Discord;

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

public class DiscordGuildLiteDto
{
  public string Id { get; set; }

  public string Name { get; set; }

  public string Icon { get; set; }

  public string Banner { get; set; }

  public bool Owner { get; set; }

  public List<SimpleDiscordPermission> Permissions { get; set; }

  public List<string> Features { get; set; }

  public int ApproximateMemberCount { get; set; }

  public int ApproximatePresenceCount { get; set; }
}

public class DiscordGuildLiteMapper : Profile
{
  public DiscordGuildLiteMapper()
  {
    CreateMap<DiscordGuildLite, DiscordGuildLiteDto>()
      .ForMember(dest => dest.Permissions, 
        opt => 
          opt.MapFrom(src => 
            DiscordPermissionHelper.ConvertToSimplePermissions(src.Permissions)))
      .ReverseMap();
  }
}