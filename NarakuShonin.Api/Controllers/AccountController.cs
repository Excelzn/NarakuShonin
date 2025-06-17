using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace NarakuShonin.Web.Controllers;

[Route("[controller]/[action]")]
public class AccountController : ControllerBase
{
  [HttpGet]
  public IActionResult Login(string returnUrl = "/")
  {
    return Challenge(new AuthenticationProperties { RedirectUri = returnUrl }, "Discord");
  }

  [HttpGet]
  public async Task<IActionResult> Logout()
  {
    await HttpContext.SignOutAsync();
    return Redirect("/");
  }
}