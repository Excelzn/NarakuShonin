using Microsoft.AspNetCore.Mvc;
using NarakuShonin.Shared;

namespace NarakuShonin.Controllers;

[ApiController]
[Route("temp")]
public class Controller1 : Controller
{
  private readonly DiscordConfig _config;

  public Controller1(DiscordConfig config)
  {
    _config = config;
  }
  
  [HttpGet]
  public IActionResult Index()
  {
    return Ok(_config);
  }
}