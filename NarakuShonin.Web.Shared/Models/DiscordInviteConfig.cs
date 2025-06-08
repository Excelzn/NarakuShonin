namespace NarakuShonin.Web.Shared.Models;

public class DiscordInviteConfig
{
  public string ClientId { get; set; }
  public string Authority { get; set; }
  public string Scopes { get; set; }
  public string Permissions { get; set; }

  public string InviteLink =>
    $"{Authority}/oauth2/authorize?client_id={ClientId}&permissions={Permissions}&integration_type=0&scope={Scopes}";
}