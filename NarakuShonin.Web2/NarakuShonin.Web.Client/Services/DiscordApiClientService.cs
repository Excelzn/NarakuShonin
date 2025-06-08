using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using NarakuShonin.Web.Shared.Models.DiscordApiModels;
using NarakuShonin.Web.Shared.Services;

namespace NarakuShonin.Web.Client.Services
{
    /// <summary>
    /// Client implementation of IDiscordApiService that calls the Discord controller endpoints
    /// </summary>
    public class DiscordApiClientService : IDiscordApiService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public DiscordApiClientService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        /// <summary>
        /// Gets the current user's guilds from Discord API via controller endpoint
        /// </summary>
        /// <returns>List of DiscordGuildLite objects</returns>
        public async Task<List<DiscordGuildLite>> GetCurrentUserGuilds()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<DiscordGuildLite>>("api/discord/guilds");
                return response ?? new List<DiscordGuildLite>();
            }
            catch (HttpRequestException ex)
            {
                // Log exception
                Console.Error.WriteLine($"Error fetching guilds: {ex.Message}");

                // If unauthorized, we could redirect to login
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    _navigationManager.NavigateTo("login", true);
                }

                throw;
            }
        }
    }
}
