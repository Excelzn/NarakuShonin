using Fluxor;
using NarakuShonin.Web.Shared.Models.DiscordApiModels;

namespace NarakuShonin.Web.Client.State.GuildManagement;

[FeatureState]
public class GuildManagementState
{
  public DiscordGuildLite? SelectedGuild { get; }
  public List<DiscordGuildLite> Guilds { get; }
  
  public bool IsLoading { get; set; }
  
  public GuildManagementState()
  {
    SelectedGuild = null;
    Guilds = [];
    IsLoading = false;
  }

  public GuildManagementState(
    List<DiscordGuildLite> guilds,
    DiscordGuildLite? selectedGuild,
    bool isLoading)
  {
    SelectedGuild = selectedGuild;
    Guilds = guilds;
    IsLoading = isLoading;
  }

  public IEnumerable<DiscordGuildLite> GetAdminGuilds()
  {
    return Guilds.Where(x =>
      DiscordPermissionHelper.HasPermission(x.Permissions,
        DiscordPermissions.Administrator));
  }
}