using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NarakuShonin.Controllers;

[ApiController]
[Authorize]
public class RootController: ControllerBase
{
  
}