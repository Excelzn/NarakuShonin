using Fluxor;
using NarakuShonin.Web.Client.State.GuildManagement.Actions;

namespace NarakuShonin.Web.Client.State.GuildManagement;

public static class GuildManagementReducers
{
  [ReducerMethod(typeof(LoadGuilds))]
  public static GuildManagementState LoadGuilds(GuildManagementState previousState)
  {
    return new GuildManagementState([], null, true);
  }

  [ReducerMethod]
  public static GuildManagementState LoadGuildsResult(GuildManagementState previousState,
    LoadGuildsResult action)
  {
    return new GuildManagementState(action.Guilds, null, false);
  }

  [ReducerMethod]
  public static GuildManagementState SelectGuild(GuildManagementState previousState,
    SelectGuild action)
  {
    var guild = previousState.Guilds.FirstOrDefault(x => x.Id == action.GuildId);
    return new GuildManagementState(previousState.Guilds, guild, false);
  }
}