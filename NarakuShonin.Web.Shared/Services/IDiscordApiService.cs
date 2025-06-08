using System.Collections.Generic;
using System.Threading.Tasks;
using NarakuShonin.Web.Shared.Models.DiscordApiModels;

namespace NarakuShonin.Web.Shared.Services
{
    /// <summary>
    /// Interface for accessing Discord API endpoints
    /// </summary>
    public interface IDiscordApiService
    {
        /// <summary>
        /// Gets the current user's guilds from Discord API
        /// </summary>
        /// <returns>List of DiscordGuildLite objects</returns>
        Task<List<DiscordGuildLite>> GetCurrentUserGuilds();
    }
}
