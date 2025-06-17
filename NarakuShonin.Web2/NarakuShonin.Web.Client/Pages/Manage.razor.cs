using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using NarakuShonin.Web.Client.State;
using NarakuShonin.Web.Client.State.GuildManagement;
using NarakuShonin.Web.Client.State.GuildManagement.Actions;

namespace NarakuShonin.Web.Client.Pages;

public partial class Manage : FluxorComponent
{
  [Parameter]
  public required string GuildId { get; set; }
  
  [Inject]
  private IState<GuildManagementState> GuildState { get; set; }

  [Inject]
  private IDispatcher Dispatcher { get; set; }

  protected override void OnInitialized()
  {
    base.OnInitialized();
    Dispatcher.Dispatch(new SelectGuild(GuildId));
  }
}