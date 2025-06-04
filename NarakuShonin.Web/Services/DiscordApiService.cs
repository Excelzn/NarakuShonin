namespace NarakuShonin.Shared.Services;

public class DiscordApiConfig
{
  public string ApiRoot { get; set; }
}

public class DiscordApiService
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
}