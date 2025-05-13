using Entity.Extension;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service.Contracts;
using Service.DTOs;

namespace Presentation.Controller;

[Route("Api/[controller]")]
[ApiController]
public class AuthenticationController(IOptions<AuthenticationDTO> authenticationDto,IServiceManager manager) : ControllerBase
{
    [HttpPost("Login")]
    public ActionResult Login([FromBody] LoginDTO loginDto)
    {
        return Ok(manager.AuthenticationService.CreateOneAuthentication(loginDto));
    }

    [HttpPost("Register")]
    public IActionResult Register([FromBody] UserDTO userDto)
    {
        return Ok(manager.AuthenticationService.CreateOneUser(userDto));
    }
}


