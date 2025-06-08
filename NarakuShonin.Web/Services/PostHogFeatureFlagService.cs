using System.Text.Json;
using System.Text.Json.Serialization;
using NarakuShonin.Web.Client.Services;
using PostHog;

namespace NarakuShonin.Web.Services;

public class PostHogFeatureFlagService: IFeatureFlagService
{
  private readonly IPostHogClient _client;

  public PostHogFeatureFlagService(IPostHogClient client)
  {
    _client = client;
  }

  public async Task<bool> IsFeatureEnabled(FeatureFlagTargetingData featureFlagTargetingData)
  {
    var phResult = await _client.IsFeatureEnabledAsync(featureFlagTargetingData.FeatureKey, featureFlagTargetingData.UserId);
    return phResult;
  }
}