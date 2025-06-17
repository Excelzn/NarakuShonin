using System.Net;
using AspNet.Security.OAuth.Discord;
using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using NarakuShonin.Web.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using MudBlazor.Services;
using NarakuShonin.Shared.Services;
using NarakuShonin.Web.Client.Services;
using NarakuShonin.Web.Services;
using NarakuShonin.Web.Shared.Models;
using NarakuShonin.Web.Shared.Services;
using PostHog;
using PostHog.Config;
using Serilog;

namespace NarakuShonin.Web;

public class Program
{
  public static async Task Main(string[] args)
  {
    Log.Logger = new LoggerConfiguration()
      .MinimumLevel.Verbose()
      .WriteTo.Console()
      .WriteTo.Seq("http://localhost:5341")
      .CreateLogger();
    try
    {
      var builder = WebApplication.CreateBuilder(args);
      builder.Services.AddSerilog();
      builder.Services.AddPostHog(opt =>
      {
        opt.PostConfigure(x =>
        {
          x.ProjectApiKey = builder.Configuration["PostHog:ProjectApiKey"];
          x.PersonalApiKey = builder.Configuration["PostHog:PersonalApiKey"];
        });
      });
      // Add services to the container.
      builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents()
        .AddInteractiveWebAssemblyComponents()
        .AddAuthenticationStateSerialization(opt =>
        {
          opt.SerializeAllClaims = true;
        });
      builder.Services.AddHttpClient();
      builder.Services.AddTransient<IFeatureFlagService, PostHogFeatureFlagService>();
      builder.Services.AddHttpContextAccessor();
 
      builder.Services.AddSingleton<DiscordInviteConfig>(_ =>
        builder.Configuration.GetSection("DiscordInviteConfig").Get<DiscordInviteConfig>() ??
        new DiscordInviteConfig());
      //Configure authentication for the user
      builder.Services.AddAuthentication(opt =>
        {
          opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
          opt.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
          opt.DefaultChallengeScheme = DiscordAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie()
        .AddDiscord(x =>
        {
          x.ClientId = builder.Configuration["Discord:ClientId"];
          x.ClientSecret = builder.Configuration["NarakuShonin:ClientSecret"];
          x.Scope.Add("guilds");

          //Required for accessing the oauth2 token in order to make requests on the user's behalf, ie. accessing the user's guild list
          x.SaveTokens = true;
        });
      builder.Services.AddAuthorization();
      builder.Services.AddSingleton<DiscordApiConfig>(_ =>
        builder.Configuration.GetSection("DiscordApi").Get<DiscordApiConfig>() ??
        new DiscordApiConfig
        {
          ApiRoot = "https://discord.com/api"
        });
      builder.Services.AddTransient<IDiscordApiService, DiscordApiService>();
      builder.Services.AddFluxor(opt =>
      {
        opt.ScanAssemblies(typeof(Program).Assembly);
        opt.UseReduxDevTools();
      });
      builder.Services.AddMudServices();

      builder.Services.AddControllers();
      var app = builder.Build();

      app.UseForwardedHeaders(new ForwardedHeadersOptions
      {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost
      });
      app.UseSerilogRequestLogging();
      app.UseAuthentication();
      app.UseAuthorization();
      // Configure the HTTP request pipeline.
      if (!app.Environment.IsDevelopment())
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }
      else
      {
        app.UseWebAssemblyDebugging();
      }

      app.UseHttpsRedirection();

      app.UseAntiforgery();

      app.MapStaticAssets();
      app.MapControllers();
      app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode()
        .AddInteractiveWebAssemblyRenderMode()
        .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

      app.Run();
    }
    catch (Exception e)
    {
      Log.Fatal(e, "Application Terminated Unexpectedly");
    }
    finally
    {
      Log.CloseAndFlush();
    }
    

  }
}