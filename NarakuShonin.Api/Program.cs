using AspNet.Security.OAuth.Discord;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using NarakuShonin.Shared.Services;

namespace NarakuShonin.Api;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddAuthentication(opt =>
      {
        opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        opt.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = DiscordAuthenticationDefaults.AuthenticationScheme;
      })
      .AddCookie(opt =>
      {
        //Causes it to 401 instead of redirecting to Discord, which would get blocked by CORS.
        opt.Events.OnRedirectToLogin = ctx =>
        {
          ctx.Response.StatusCode = 401;
          return Task.CompletedTask;
        };
      })
      .AddDiscord(x =>
      {
        x.Events.OnRedirectToAuthorizationEndpoint = ctx =>
        {
          if (ctx.Request.Path.StartsWithSegments("/api"))
          {
            ctx.Response.Clear();
            ctx.Response.StatusCode = 401;
            return Task.CompletedTask;
          }
          ctx.Response.Redirect(ctx.RedirectUri);
          return Task.CompletedTask;
        };
        x.ClientId = builder.Configuration["Discord:ClientId"];
        x.ClientSecret = builder.Configuration["NarakuShonin:ClientSecret"];
        x.Scope.Add("guilds");

        //Required for accessing the oauth2 token in order to make requests on the user's behalf, ie. accessing the user's guild list
        x.SaveTokens = true;
      });
    builder.Services.AddHttpContextAccessor();
    // Add services to the container.
    builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(Program).Assembly));
    builder.Services.AddHttpClient<IDiscordApiService, DiscordApiService>();
    builder.Services.AddSingleton<DiscordApiConfig>(_ =>
      builder.Configuration.GetSection("DiscordApi").Get<DiscordApiConfig>() ??
      new DiscordApiConfig
      {
        ApiRoot = "https://discord.com/api"
      });
    
    builder.Services.AddControllers();
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
      app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.MapFallbackToFile("index.html");
    app.Run();
  }
}