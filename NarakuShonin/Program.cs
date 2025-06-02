using System.Globalization;
using AspNet.Security.OAuth.Discord;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using NarakuShonin.Shared;

namespace NarakuShonin;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();
    builder.Services.AddTransient<DiscordConfig>(_ => new DiscordConfig
    {
      ClientId = builder.Configuration["Discord:ClientId"],
      ClientSecret = builder.Configuration["NarakuShonin:ClientSecret"],
    });

    builder.Services.AddAuthentication(opt =>
    {
      opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
      opt.DefaultChallengeScheme = DiscordAuthenticationDefaults.AuthenticationScheme;
    }).AddDiscord(options =>
    {
      options.ClientId = builder.Configuration["Discord:ClientId"];
      options.ClientSecret = builder.Configuration["NarakuShonin:ClientSecret"];
      options.CallbackPath = discordOptions.CallBack;
      options.SaveTokens = true;

      options.CorrelationCookie.SameSite = SameSiteMode.Lax;
      options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;

      options.ClaimActions.MapCustomJson("urn:discord:avatar:url", user =>
        string.Format(
          CultureInfo.InvariantCulture,
          "https://cdn.discordapp.com/avatars/{0}/{1}.{2}",
          user.GetString("id"),
          user.GetString("avatar"),
          user.GetString("avatar")!.StartsWith("a_") ? "gif" : "png"));
    
      options.Scope.Add("identify");
      options.Scope.Add("email");

    });
    
    var app = builder.Build();
    app.UseAuthentication();
    
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
      app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
  }
}