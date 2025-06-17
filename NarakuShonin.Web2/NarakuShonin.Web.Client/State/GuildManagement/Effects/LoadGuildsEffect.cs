using Fluxor;
using NarakuShonin.Web.Client.State.GuildManagement.Actions;
using NarakuShonin.Web.Shared.Services;

namespace NarakuShonin.Web.Client.State.GuildManagement.Effects;

public class LoadGuildsEffect: Effect<LoadGuilds>
{
  private readonly IDiscordApiService _discordApiService;
  private readonly IDispatcher _dispatcher;

  public LoadGuildsEffect(IDiscordApiService discordApiService, IDispatcher dispatcher)
  {
    _discordApiService = discordApiService;
    _dispatcher = dispatcher;
  }
  
  public override async Task HandleAsync(LoadGuilds action, IDispatcher dispatcher)
  {
    var result = await _discordApiService.GetCurrentUserGuilds();
    _dispatcher.Dispatch(new LoadGuildsResult(result));
  }
}