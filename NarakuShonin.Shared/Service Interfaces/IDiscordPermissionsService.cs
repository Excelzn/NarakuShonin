using NarakuShonin.Web.Shared.Models.DiscordApiModels;

namespace NarakuShonin.Shared.Service_Interfaces;

public interface IDiscordPermissionsService
{
  Task<bool> HasPermission(string guildId, DiscordPermissions permission);
}