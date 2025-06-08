using System.Net.Http.Json;

namespace NarakuShonin.Web.Client.Services;

public class FeatureFlagClientService: IFeatureFlagService
{
  private readonly HttpClient _httpClient;

  public FeatureFlagClientService(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }
  
  public async Task<bool> IsFeatureEnabled(FeatureFlagTargetingData featureFlagTargetingData)
  {
    var uri = $"/api/featureflags/{featureFlagTargetingData.FeatureKey}";
    var result = await _httpClient.GetFromJsonAsync<FeatureFlagResult>(uri);
    return result?.Result ?? false;
  }
}