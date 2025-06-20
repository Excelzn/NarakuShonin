using NarakuShonin.Api.Models;

namespace NarakuShonin.Shared.Repositories;

public interface IGuildConfigurationRepository
{
  Task<GuildConfigurationDto?> GetGuildConfiguration(string guildId);
  Task CreateGuildConfiguration(GuildConfigurationDto guildConfiguration);
  Task<List<GuildConfigurationDto>> GetUserGuildConfigurations(List<string> guildIds);
}