using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NarakuShonin.Web.Shared.Models.DiscordApiModels;
using NarakuShonin.Web.Shared.Services;

namespace NarakuShonin.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DiscordController : ControllerBase
{
  private readonly IDiscordApiService _discordApiService;

  public DiscordController(IDiscordApiService discordApiService)
  {
    _discordApiService = discordApiService;
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
      return Ok(guilds);
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