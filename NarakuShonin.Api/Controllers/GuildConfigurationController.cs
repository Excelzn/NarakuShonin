using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NarakuShonin.Api.Models;
using NarakuShonin.Shared.Repositories;
using NarakuShonin.Shared.Service_Interfaces;
using NarakuShonin.Web.Shared.Models.DiscordApiModels;

namespace NarakuShonin.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/guildconfiguration")]
public class GuildConfigurationController: ControllerBase
{
  private readonly IGuildConfigurationRepository _guildConfigurationRepository;
  private readonly IDiscordPermissionsService _discordPermissionsService;

  public GuildConfigurationController(
    IGuildConfigurationRepository guildConfigurationRepository,
    IDiscordPermissionsService discordPermissionsService
    )
  {
    _guildConfigurationRepository = guildConfigurationRepository;
    _discordPermissionsService = discordPermissionsService;
  }
  
  [HttpPost]
  public async Task<IActionResult> PostGuildConfiguration([FromBody] GuildConfigurationDto guildConfiguration)
  {
    //validate user has access and permissions to guild.
    var hasPermission =
      await _discordPermissionsService.HasPermission(guildConfiguration.Id,
        DiscordPermissions.Administrator);
    if (!hasPermission)
    {
      return BadRequest();
    }
    await _guildConfigurationRepository.CreateGuildConfiguration(guildConfiguration);
    return NoContent();
  }
}