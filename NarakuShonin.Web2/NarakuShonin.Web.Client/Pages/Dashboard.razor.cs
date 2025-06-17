using System.Security.Claims;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using NarakuShonin.Web.Client.Services;
using NarakuShonin.Web.Client.State;
using NarakuShonin.Web.Client.State.GuildManagement;
using NarakuShonin.Web.Client.State.GuildManagement.Actions;
using NarakuShonin.Web.Shared.Models;
using NarakuShonin.Web.Shared.Models.DiscordApiModels;
using NarakuShonin.Web.Shared.Services;

namespace NarakuShonin.Web.Client.Pages;

public partial class Dashboard : FluxorComponent
{
  private readonly IFeatureFlagService _featureFlagService;
  private readonly AuthenticationStateProvider _authenticationState;
  private readonly DiscordInviteConfig _discordInviteConfig;

  private bool AllowInvitingBot { get; set; }
  
  [Inject]
  private IState<GuildManagementState> GuildManagementState { get; set; }
  [Inject]
  private IDispatcher Dispatcher { get; set; }

  public Dashboard(
    IFeatureFlagService featureFlagService,
    AuthenticationStateProvider authenticationState,
    DiscordInviteConfig discordInviteConfig
    )
  {
    _featureFlagService = featureFlagService;
    _authenticationState = authenticationState;
    _discordInviteConfig = discordInviteConfig;
  }

  protected override void OnInitialized()
  {
    base.OnInitialized();
    Dispatcher.Dispatch(new LoadGuilds());
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
    await base.OnInitializedAsync();
  }

  private string GetGuildIcon(DiscordGuildLite guild)
  {
    return $"https://cdn.discordapp.com/icons/{guild.Id}/{guild.Icon}.png";
  }
}