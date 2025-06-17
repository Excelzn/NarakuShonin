namespace NarakuShonin.Web.Client.State.GuildManagement.Actions;

public class SelectGuild
{
  public string GuildId { get; }

  public SelectGuild(string guildId)
  {
    GuildId = guildId;
  }
}