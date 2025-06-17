using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NarakuShonin.Web.Controllers;
[Authorize]
[ApiController]
[Route("/api/test")]
public class TestController: ControllerBase
{
  [HttpGet]
  public IActionResult Test()
  {
    return Ok(new {Value = "WORKS"});
  }
}