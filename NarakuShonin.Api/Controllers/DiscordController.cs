using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NarakuShonin.Api.Models.Discord;
using NarakuShonin.Shared.Repositories;
using NarakuShonin.Shared.Service_Interfaces;
using NarakuShonin.Shared.Services;

namespace NarakuShonin.Api.Controllers;

[ApiController]
[Route("api/discord")]
[Authorize]
public class DiscordController : ControllerBase
{
  private readonly IDiscordApiService _discordApiService;
  private readonly IGuildConfigurationRepository _guildConfigurationRepository;

  public DiscordController(IDiscordApiService discordApiService, IGuildConfigurationRepository guildConfigurationRepository)
  {
    _discordApiService = discordApiService;
    _guildConfigurationRepository = guildConfigurationRepository;
  }

  /// <summary>
  /// Gets the current user's Discord guilds
  /// </summary>
  /// <returns>A list of guilds the user is a member of</returns>
  [HttpGet("guilds")]
  public async Task<ActionResult<List<DiscordGuildLite>>> GetGuilds()
  {
    try
    {
      var guilds = await _discordApiService.GetCurrentUserGuilds();
      var dtos = guilds.Select(x => x.GetDto()).ToList();
      var guildIds = guilds.Select(x => x.Id).ToList();
      var configuredGuilds = await _guildConfigurationRepository.GetUserGuildConfigurations(guildIds);
      foreach (var guild in dtos)
      {
        guild.RegisteredWithBot = configuredGuilds.Any(x => x.Id == guild.Id);
      }
      return Ok(dtos);
    }
    catch (UnauthorizedAccessException ex)
    {
      return Unauthorized(ex.Message);
    }
    catch (HttpRequestException ex)
    {
      return StatusCode(500, $"Error communicating with Discord API: {ex.Message}");
    }
  }
}