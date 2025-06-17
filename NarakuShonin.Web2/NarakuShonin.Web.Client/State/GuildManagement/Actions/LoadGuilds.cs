using NarakuShonin.Web.Shared.Models.DiscordApiModels;

namespace NarakuShonin.Web.Client.State.GuildManagement.Actions;

public class LoadGuilds
{
  public LoadGuilds()
  {
    
  }
}

public class LoadGuildsResult
{
  public LoadGuildsResult(List<DiscordGuildLite> guilds)
  {
    Guilds = guilds;
  }

  public List<DiscordGuildLite> Guilds { get; set; }
}