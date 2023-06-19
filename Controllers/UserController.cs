using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Mappy.Models.Responses;
using Mappy.Services;
using Microsoft.AspNetCore.Authorization;

namespace Mappy.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
  //=============================================================================================
  private readonly UserService _userService;


  //=============================================================================================
  public UserController(UserService userService)
  {
    _userService = userService;
  }



  //=============================================================================================
  [HttpGet("")]
  public async Task<IActionResult> GetUserById()
  {
    var id = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.UserData)?.Value;

    if (id == null)
    {
      return BadRequest(new { message = "Cannot get the Id of the user..." });
    }

    var result = await _userService.GetUserById(id);

    if (!result.IsSuccessful)
    {
      return NotFound(new { message = result.Message });
    }
    else
    {
      var user = result.Data as SecureUserModel;

      return Ok(new { Username = user.Username, Email = user.Email });
    }
  }
}