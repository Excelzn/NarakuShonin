using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NarakuShonin.Api.Models;

namespace NarakuShonin.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/user")]
public class UserController: ControllerBase
{
  [HttpGet]
  public IActionResult GetUser()
  {
    var claims = HttpContext.User.Claims
      .Select(c => new UserClaim(c.Type, c.Value)).ToList();
    return Ok(claims);
  }
}