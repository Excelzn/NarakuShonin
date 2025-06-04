using System.Security.Claims;
using NarakuShonin.Web.Shared.Models;

namespace NarakuShonin.Web.Client.Layout;

public partial class MainLayout
{
  private bool _isOpen = true;
  
  private void ToggleDrawer() => _isOpen = !_isOpen;

  private string GetAvatar(ClaimsPrincipal user)
  {
    var avatar = user.Claims.FirstOrDefault(c => c.Type == DiscordClaimTypes.Avatar)?.Value;
    var userid = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    if (avatar != null && userid != null)
    {
      return $"https://cdn.discordapp.com/avatars/{userid}/{avatar}.png";
    }

    return "";
  }
}