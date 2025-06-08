namespace NarakuShonin.Web.Client.Services;

public interface IFeatureFlagService
{
  Task<bool> IsFeatureEnabled(FeatureFlagTargetingData featureFlagTargetingData);
}

public class FeatureFlagTargetingData
{
  public string FeatureKey { get; set; }
  public string UserId { get; set; }
}

public class FeatureFlagResult
{
  public bool Result { get; set; }
}