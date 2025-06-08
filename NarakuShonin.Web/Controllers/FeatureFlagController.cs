using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NarakuShonin.Web.Client.Services;

namespace NarakuShonin.Web.Controllers;

[ApiController]
[Route("/api/featureFlags")]
[Authorize]
public class FeatureFlagController : ControllerBase
{
  private readonly IFeatureFlagService _featureFlagService;

  public FeatureFlagController(IFeatureFlagService featureFlagService)
  {
    _featureFlagService = featureFlagService;
  }
  
  [HttpGet("{featureKey}")]
  public async Task<IActionResult> Index(string featureKey)
  {
    var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    var targetingData = new FeatureFlagTargetingData
    {
      FeatureKey = featureKey,
      UserId = userId
    };
    var result = await _featureFlagService.IsFeatureEnabled(targetingData);
    return Ok(new FeatureFlagResult{Result=result});
  }
}