using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NarakuShonin.Api.Models.Discord;
using NarakuShonin.Shared.Services;

namespace NarakuShonin.Api.Controllers;

[ApiController]
[Route("api/discord")]
[Authorize]
public class DiscordController : ControllerBase
{
  private readonly IDiscordApiService _discordApiService;
  private readonly IMapper _mapper;

  public DiscordController(IDiscordApiService discordApiService, IMapper mapper)
  {
    _discordApiService = discordApiService;
    _mapper = mapper;
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
      return Ok(_mapper.Map<List<DiscordGuildLiteDto>>(guilds));
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