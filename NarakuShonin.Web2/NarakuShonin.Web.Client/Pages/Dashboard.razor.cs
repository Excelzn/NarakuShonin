using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using NarakuShonin.Web.Client.Services;
using NarakuShonin.Web.Shared.Models;
using NarakuShonin.Web.Shared.Models.DiscordApiModels;
using NarakuShonin.Web.Shared.Services;

namespace NarakuShonin.Web.Client.Pages;

public partial class Dashboard : ComponentBase
{
  private readonly IFeatureFlagService _featureFlagService;
  private readonly AuthenticationStateProvider _authenticationState;
  private readonly DiscordInviteConfig _discordInviteConfig;
  private readonly IDiscordApiService _discordApiService;
  
  private bool AllowInvitingBot { get; set; }
  private List<DiscordGuildLite> Guilds { get; set; }

  public Dashboard(
    IFeatureFlagService featureFlagService,
    AuthenticationStateProvider authenticationState,
    DiscordInviteConfig discordInviteConfig,
    IDiscordApiService discordApiService
    )
  {
    _featureFlagService = featureFlagService;
    _authenticationState = authenticationState;
    _discordInviteConfig = discordInviteConfig;
    _discordApiService = discordApiService;
  }

  protected override async Task OnInitializedAsync()
  {
    var authState = await _authenticationState.GetAuthenticationStateAsync();
    AllowInvitingBot = await _featureFlagService.IsFeatureEnabled(new FeatureFlagTargetingData
    {
      FeatureKey = FeatureFlagKeys.AllowInvitingBot,
      UserId = authState.User.Claims
        .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? ""
    });
    Guilds = await _discordApiService.GetCurrentUserGuilds();
  }

  private string GetGuildIcon(DiscordGuildLite guild)
  {
    return $"https://cdn.discordapp.com/icons/{guild.Id}/{guild.Icon}.png";
  }
}