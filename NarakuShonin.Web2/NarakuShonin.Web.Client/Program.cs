using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using NarakuShonin.Web.Client.Services;
using NarakuShonin.Web.Shared.Models;
using NarakuShonin.Web.Shared.Services;
using Statsig.Client;

namespace NarakuShonin.Web.Client;

class Program
{
  static async Task Main(string[] args)
  {
    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.Services.AddAuthorizationCore();
    builder.Services.AddCascadingAuthenticationState();
    builder.Services.AddAuthenticationStateDeserialization();
    builder.Services.AddMudServices();
    builder.Services.AddFluxor(opt =>
    {
      opt.ScanAssemblies(typeof(Program).Assembly);
      opt.UseReduxDevTools();
    });
    builder.Services.AddSingleton<DiscordInviteConfig>(_ =>
      builder.Configuration.GetSection("DiscordInviteConfig").Get<DiscordInviteConfig>() ??
      new DiscordInviteConfig());

    builder.Services.AddHttpClient<IDiscordApiService, DiscordApiClientService>(client => 
      client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
    builder.Services.AddHttpClient<IFeatureFlagService, FeatureFlagClientService>(client => 
      client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));


    await builder.Build().RunAsync();
  }
}