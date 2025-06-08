using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NarakuShonin.Web.Shared.Models.DiscordApiModels;
using NarakuShonin.Web.Shared.Services;

namespace NarakuShonin.Shared.Services;

public class DiscordApiConfig
{
  public string ApiRoot { get; set; }
}

public class DiscordApiService : IDiscordApiService
{
  private readonly IHttpContextAccessor _accessor;
  private readonly HttpClient _httpClient;
  private readonly DiscordApiConfig _config;

  public DiscordApiService(IHttpContextAccessor accessor, HttpClient httpClient, DiscordApiConfig config)
  {
    _accessor = accessor;
    _httpClient = httpClient;
    _config = config;
  }

  /// <summary>
  /// Gets the current user's guilds from Discord API
  /// </summary>
  /// <returns>List of DiscordGuildLite objects</returns>
  public async Task<List<DiscordGuildLite>> GetCurrentUserGuilds()
  {
    var httpContext = _accessor.HttpContext;
    if (httpContext == null)
    {
      throw new UnauthorizedAccessException("User must be authenticated to access Discord guilds");
    }

    // Get the access token from the user's authentication
    var accessToken = await httpContext.GetTokenAsync("access_token");
    if (string.IsNullOrEmpty(accessToken))
    {
      throw new UnauthorizedAccessException("Access token not found");
    }

    // Set up the request to the Discord API
    var request = new HttpRequestMessage(HttpMethod.Get, $"{_config.ApiRoot}/users/@me/guilds");
    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

    // Make the request
    var response = await _httpClient.SendAsync(request);

    // Check if the request was successful
    if (!response.IsSuccessStatusCode)
    {
      throw new HttpRequestException($"Error getting guilds: {response.StatusCode}");
    }

    // Parse the response
    var content = await response.Content.ReadAsStringAsync();
    var guilds = JsonSerializer.Deserialize<List<DiscordGuildLite>>(content, new JsonSerializerOptions
    {
      PropertyNameCaseInsensitive = true
    });

    return guilds ?? new List<DiscordGuildLite>();
  }
}