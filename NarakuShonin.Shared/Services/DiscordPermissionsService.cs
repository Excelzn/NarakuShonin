using NarakuShonin.Shared.Service_Interfaces;
using NarakuShonin.Web.Shared.Models.DiscordApiModels;

namespace NarakuShonin.Shared.Services;

public class DiscordPermissionsService : IDiscordPermissionsService
{
  private readonly IDiscordApiService _discordApiService;

  public DiscordPermissionsService(IDiscordApiService discordApiService)
  {
    _discordApiService = discordApiService;
  }

  public async Task<bool> HasPermission(string guildId, DiscordPermissions permission)
  {
    //grab user's guilds
    var guilds = await _discordApiService.GetCurrentUserGuilds();
    //find guild to check
    var guild = guilds.FirstOrDefault(x => x.Id == guildId);
    if (guild == null)
    {
      return false; //user is not a member of this guild
    }
    var permissions = guild.Permissions;
    return DiscordPermissionHelper.HasPermission(permissions, permission);
  }
}