using NarakuShonin.Api.Models.Discord;

namespace NarakuShonin.Shared.Services;

public interface IDiscordApiService
{
  /// <summary>
  /// Gets the current user's guilds from Discord API
  /// </summary>
  /// <returns>List of DiscordGuildLite objects</returns>
  Task<List<DiscordGuildLite>> GetCurrentUserGuilds();
}