using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Service.DTOs;
namespace Presentation.Controller;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController(IServiceManager manager) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    public ActionResult GetAllUsers()
    {
        return Ok(manager.UserService.GetAllUsers());
    }

    [HttpGet("Profile")]
    public IActionResult GetOneUser()
    {
        return Ok(manager.UserService.GetOneUser());
    }

    [HttpPut]
    public IActionResult UpdateOneUser([FromBody] UserDTO userDto)
    {
        return Ok(manager.UserService.UpdateOneUser(userDto));
    }

    [HttpDelete]
    public IActionResult DeleteOneUser()
    {
        return Ok(manager.UserService.DeleteOneUser());
    }
}